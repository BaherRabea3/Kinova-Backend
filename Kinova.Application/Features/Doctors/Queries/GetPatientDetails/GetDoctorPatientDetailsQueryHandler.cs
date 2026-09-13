using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Doctors.Queries.GetPatientDetails
{
    public sealed class GetDoctorPatientDetailsQueryHandler : IRequestHandler<GetDoctorPatientDetailsQuery, Result<DoctorPatientDetailsDto>>
    {
        private readonly IKinovaDbContext _context;

        public GetDoctorPatientDetailsQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<DoctorPatientDetailsDto>> Handle(GetDoctorPatientDetailsQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.UserId == request.DoctorUserId, cancellationToken);

            if (doctor is null)
                return Result.Failure<DoctorPatientDetailsDto>(DoctorErrors.UnAuthorized());

            var patient = await _context.Patients
                .AsNoTracking()
                .Include(p => p.Plans)
                    .ThenInclude(pl => pl.Exercises)
                        .ThenInclude(e => e.Exercise)
                .Include(p => p.Sessions)
                    .ThenInclude(s => s.Exercise)
                .Include(p => p.Sessions)
                    .ThenInclude(s => s.Score)
                .FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);

            if (patient is null)
                return Result.Failure<DoctorPatientDetailsDto>(DoctorErrors.PatientNotFound());

            if (patient.DoctorId != doctor.Id)
                return Result.Failure<DoctorPatientDetailsDto>(DoctorErrors.NotYourPatient());

            var dto = new DoctorPatientDetailsDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Gender = patient.Gender,
                Height = patient.Height,
                Weight = patient.Weight,
                CarePath = patient.CarePath,
                Status = patient.Plans.Any(pl => pl.IsActive) ? "Active" : "Inactive",
                Plans = patient.Plans
                    .OrderByDescending(pl => pl.StartDate)
                    .Select(pl => new PlanResponseDto
                    {
                        Id = pl.Id,
                        Name = pl.Name,
                        Description = pl.Description,
                        Source = pl.Source.ToString(),
                        StartDate = pl.StartDate,
                        EndDate = pl.EndDate,
                        IsActive = pl.IsActive,
                        DoctorId = pl.DoctorId,
                        DoctorName = doctor.Name,
                        Exercises = pl.Exercises.Select(e => new PlanExerciseItemDto
                        {
                            Id = e.Id,
                            Sets = e.Sets,
                            Repetitions = e.Repetitions,
                            FrequencyPerWeek = e.FrequencyPerWeek,
                            Exercise = new ExerciseSummaryDto
                            {
                                Id = e.ExerciseId,
                                Name = e.Exercise.Name,
                                Category = e.Exercise.Category,
                                BodyPart = e.Exercise.BodyPart,
                                DifficultyLevel = e.Exercise.DifficultyLevel,
                                VideoUrl = e.Exercise.VideoUrl
                            }
                        }).ToList()
                    }).ToList(),
                RecentSessions = patient.Sessions
                    .OrderByDescending(s => s.SessionDate)
                    .ThenByDescending(s => s.StartTime)
                    .Take(20)
                    .Select(s => new PatientSessionHistoryDto
                    {
                        Id = s.Id,
                        SessionDate = s.SessionDate,
                        Status = s.Status.ToString(),
                        ExerciseName = s.Exercise.Name,
                        OverallScore = s.Score != null ? s.Score.OverallScore : null,
                        ValidRepetitions = s.Score != null ? s.Score.ValidRepetitions : null,
                        InvalidRepetitions = s.Score != null ? s.Score.InvalidRepetitions : null
                    }).ToList()
            };

            return Result.Success(dto);
        }
    }
}
