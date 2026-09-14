using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.Plans;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Doctors.Commands.UpdatePlan
{
    public sealed class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, Result<PlanResponseDto>>
    {
        private readonly IKinovaDbContext _context;

        public UpdatePlanCommandHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PlanResponseDto>> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.UserId == request.DoctorUserId, cancellationToken);

            if (doctor is null)
                return Result.Failure<PlanResponseDto>(DoctorErrors.UnAuthorized());

            var plan = await _context.Plans
                .Include(p => p.Exercises)
                    .ThenInclude(e => e.Exercise)
                .FirstOrDefaultAsync(p => p.Id == request.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure<PlanResponseDto>(PlanErrors.NotFound());

            if (plan.DoctorId != doctor.Id)
                return Result.Failure<PlanResponseDto>(PlanErrors.NotYourPlan());

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            // MVP rule: only one active plan per patient.
            if (request.IsActive && !plan.IsActive)
            {
                var currentlyActivePlans = await _context.Plans
                    .Where(p => p.PatientId == plan.PatientId && p.IsActive && p.Id != plan.Id)
                    .ToListAsync(cancellationToken);

                foreach (var activePlan in currentlyActivePlans)
                    activePlan.IsActive = false;
            }

            plan.Name = request.Name;
            plan.Description = request.Description;
            plan.StartDate = request.StartDate;
            plan.EndDate = request.EndDate;
            plan.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Result.Success(new PlanResponseDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Source = plan.Source.ToString(),
                StartDate = plan.StartDate,
                EndDate = plan.EndDate,
                IsActive = plan.IsActive,
                DoctorId = plan.DoctorId,
                DoctorName = doctor.Name,
                Exercises = plan.Exercises.Select(e => new PlanExerciseItemDto
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
