
using Kinova.Application.Common.DTOs.SessionDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Exercises;
using Kinova.Domain.Entities.PlanExerciseItems;
using Kinova.Domain.Entities.Sessions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Sessions.Commands.StartSessionByExerciseId
{
    public sealed class StartSessionByExerciseIdCommandHandler : IRequestHandler<StartSessionByExerciseIdCommand, Result<StartSessionDto>>
    {
        private readonly IKinovaDbContext _context;

        public StartSessionByExerciseIdCommandHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<StartSessionDto>> Handle(StartSessionByExerciseIdCommand request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients.FirstAsync(p => p.UserId == request.UserId, cancellationToken);

            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(i => i.Id == request.ExerciseId, cancellationToken);

            if (exercise is null) return Result.Failure<StartSessionDto>(ExerciseErrors.NotFound);

            var session = new Session
            {
                Id = Guid.NewGuid(),
                PatientId = patient.Id,
                ExerciseId = request.ExerciseId,
                SessionDate = DateOnly.FromDateTime(DateTime.UtcNow),
                StartTime = DateTime.UtcNow,
                Status = SessionStatus.InProgress
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new StartSessionDto
            {
                Id = session.Id,
                ExerciseId = session.ExerciseId,
                ExerciseName = exercise.Name,
                SessionDate = session.SessionDate,
                StartTime = session.StartTime,
                Status = session.Status.ToString(),
                Defaults = exercise.Defaults
            };

            return Result.Success(response);
        }
    }
}
