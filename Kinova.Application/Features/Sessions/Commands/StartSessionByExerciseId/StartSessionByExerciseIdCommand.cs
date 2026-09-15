
using Kinova.Application.Common.DTOs.SessionDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Sessions.Commands.StartSessionByExerciseId
{
    public sealed record StartSessionByExerciseIdCommand(Guid UserId, Guid ExerciseId) : IRequest<Result<StartSessionDto>>
    {
    }
}
