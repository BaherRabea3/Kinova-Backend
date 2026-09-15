using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Doctors.Queries.GetPatientPlans
{
    public sealed class GetPatientPlansQueryHandler : IRequestHandler<GetPatientPlansQuery, Result<List<PlanResponseDto>>>
    {
        private readonly IKinovaDbContext _context;

        public GetPatientPlansQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<PlanResponseDto>>> Handle(GetPatientPlansQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.UserId == request.DoctorUserId, cancellationToken);

            if (doctor is null)
                return Result.Failure<List<PlanResponseDto>>(DoctorErrors.UnAuthorized());

            var patient = await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);

            if (patient is null)
                return Result.Failure<List<PlanResponseDto>>(DoctorErrors.PatientNotFound());

            if (patient.DoctorId != doctor.Id)
                return Result.Failure<List<PlanResponseDto>>(DoctorErrors.NotYourPatient());

            var plans = await _context.Plans
                .AsNoTracking()
                .Include(p => p.Doctor)
                .Include(p => p.Exercises)
                    .ThenInclude(e => e.Exercise)
                .Where(p => p.PatientId == patient.Id)
                .OrderByDescending(p => p.IsActive)
                .ThenByDescending(p => p.StartDate)
                .Select(p => new PlanResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Source = p.Source.ToString(),
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    IsActive = p.IsActive,
                    DoctorId = p.DoctorId,
                    DoctorName = p.Doctor.Name,
                    Exercises = p.Exercises.Select(e => new PlanExerciseItemDto
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
                })
                .ToListAsync(cancellationToken);

            return Result.Success(plans);
        }
    }
}
