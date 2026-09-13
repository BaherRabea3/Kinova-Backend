using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.PlanExerciseItems;
using Kinova.Domain.Entities.Plans;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Doctors.Commands.CreatePlan
{
    public sealed class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, Result<PlanResponseDto>>
    {
        private readonly IKinovaDbContext _context;

        public CreatePlanCommandHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PlanResponseDto>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.UserId == request.DoctorUserId, cancellationToken);

            if (doctor is null)
                return Result.Failure<PlanResponseDto>(DoctorErrors.UnAuthorized());

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);

            if (patient is null)
                return Result.Failure<PlanResponseDto>(DoctorErrors.PatientNotFound());

            if (patient.DoctorId != doctor.Id)
                return Result.Failure<PlanResponseDto>(DoctorErrors.NotYourPatient());

            var exerciseIds = request.Exercises.Select(e => e.ExerciseId).Distinct().ToList();

            var existingExerciseIds = await _context.Exercises
                .Where(e => exerciseIds.Contains(e.Id))
                .Select(e => e.Id)
                .ToListAsync(cancellationToken);

            var missingExerciseId = exerciseIds.FirstOrDefault(id => !existingExerciseIds.Contains(id));
            if (missingExerciseId != Guid.Empty)
                return Result.Failure<PlanResponseDto>(PlanErrors.ExerciseNotFound(missingExerciseId));

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            // MVP rule: only one active plan per patient.
            if (request.IsActive)
            {
                var currentlyActivePlans = await _context.Plans
                    .Where(p => p.PatientId == patient.Id && p.IsActive)
                    .ToListAsync(cancellationToken);

                foreach (var activePlan in currentlyActivePlans)
                    activePlan.IsActive = false;
            }

            var plan = new Plan
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Source = PlanSource.Doctor,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsActive = request.IsActive,
                DoctorId = doctor.Id,
                PatientId = patient.Id
            };

            foreach (var item in request.Exercises)
            {
                plan.Exercises.Add(new PlanExerciseItem
                {
                    Id = Guid.NewGuid(),
                    PlanId = plan.Id,
                    ExerciseId = item.ExerciseId,
                    Sets = item.Sets,
                    Repetitions = item.Repetitions,
                    FrequencyPerWeek = item.FrequencyPerWeek
                });
            }

            _context.Plans.Add(plan);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var createdPlan = await _context.Plans
                .AsNoTracking()
                .Include(p => p.Exercises)
                    .ThenInclude(e => e.Exercise)
                .FirstAsync(p => p.Id == plan.Id, cancellationToken);

            return Result.Success(new PlanResponseDto
            {
                Id = createdPlan.Id,
                Name = createdPlan.Name,
                Description = createdPlan.Description,
                Source = createdPlan.Source.ToString(),
                StartDate = createdPlan.StartDate,
                EndDate = createdPlan.EndDate,
                IsActive = createdPlan.IsActive,
                DoctorId = createdPlan.DoctorId,
                DoctorName = doctor.Name,
                Exercises = createdPlan.Exercises.Select(e => new PlanExerciseItemDto
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
            });
        }
    }
}
