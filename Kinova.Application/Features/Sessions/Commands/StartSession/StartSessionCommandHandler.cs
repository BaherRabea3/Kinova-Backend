
using Kinova.Application.Common.DTOs.SessionDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.PlanExerciseItems;
using Kinova.Domain.Entities.Sessions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kinova.Application.Features.Sessions.Commands.StartSession
{
    public class StartSessionHandler : IRequestHandler<StartSessionCommand, Result<StartSessionDto>>
    {
        private readonly IKinovaDbContext _context;
        public StartSessionHandler(IKinovaDbContext db) => _context = db;

        public async Task<Result<StartSessionDto>> Handle(StartSessionCommand cmd, CancellationToken ct)
        {
            var patient = await _context.Patients.FirstAsync(p => p.UserId == cmd.UserId, ct);

            var item = await _context.PlanExercises
                .Include(i => i.Plan)
                .Include(x => x.Exercise)
                .FirstOrDefaultAsync(i => i.Id == cmd.PlanExerciseItemId
                                       && i.Plan.PatientId == patient.Id
                                       && i.Plan.IsActive, ct);

            if (item is null) return Result.Failure<StartSessionDto>(PlanExerciseItemErrors.NotInPatientPlan());

            var session = new Session
            {
                Id = Guid.NewGuid(),
                PatientId = patient.Id,
                ExerciseId = item.ExerciseId,
                ItemId = item.Id,
                SessionDate = DateOnly.FromDateTime(DateTime.UtcNow),
                StartTime = DateTime.UtcNow,
                Status = SessionStatus.InProgress
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync(ct);

            var response = new StartSessionDto
            {
                Id = session.Id,
                ExerciseId = session.ExerciseId,
                ExerciseName = item.Exercise.Name,
                PlanExerciseItemId = session.ItemId,
                SessionDate = session.SessionDate,
                StartTime = session.StartTime,
                Status = session.Status.ToString(),
                TargetReps = item.Repetitions,
                TargetSets = item.Sets,
            };

            return Result.Success(response);
        }
    }
}
