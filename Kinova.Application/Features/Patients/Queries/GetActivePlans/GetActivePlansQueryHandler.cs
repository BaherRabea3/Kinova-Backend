
using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Patients.Queries.GetActivePlans
{
    public sealed class GetActivePlansQueryHandler : IRequestHandler<GetActivePlansQuery, Result<List<PlanResponseDto>>>
    {
        private readonly IKinovaDbContext _context;

        public GetActivePlansQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<PlanResponseDto>>> Handle(GetActivePlansQuery request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients
                .FirstAsync(x => x.UserId == request.UserId, cancellationToken);

            var planResponse = await _context.Plans
                                    .AsNoTracking()
                                    .Include(x => x.Doctor)
                                    .Include(x => x.Exercises)
                                        .ThenInclude(x => x.Exercise)
                                    .Where(x => x.PatientId == patient.Id && x.IsActive == true)
                                    .OrderByDescending(x => x.StartDate)
                                    .Select(x => new PlanResponseDto
                                    {
                                        Id = x.Id,
                                        StartDate = x.StartDate,
                                        EndDate = x.EndDate,
                                        Name = x.Name,
                                        Description = x.Description,
                                        Source = x.Source.ToString(),
                                        DoctorId = x.DoctorId,
                                        DoctorName = x.Doctor.Name,
                                        Exercises = x.Exercises.Select(e => new PlanExerciseItemDto
                                        {
                                            Id = e.Id,
                                            Sets = e.Sets,
                                            Repetitions = e.Repetitions,
                                            FrequencyPerWeek = e.FrequencyPerWeek,
                                            Exercise = new ExerciseSummaryDto
                                            {
                                                Id  = e.ExerciseId,
                                                Name =  e.Exercise.Name,
                                                Category = e.Exercise.Category,
                                                BodyPart = e.Exercise.BodyPart,
                                                DifficultyLevel = e.Exercise.DifficultyLevel,
                                                VideoUrl = e.Exercise.VideoUrl
                                            }
                                        }).ToList()
                                    }).ToListAsync(cancellationToken);

            return Result.Success(planResponse);
        }
    }
}
