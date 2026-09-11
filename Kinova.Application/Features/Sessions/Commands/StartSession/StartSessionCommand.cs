using Kinova.Application.Common.DTOs.SessionDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Sessions.Commands.StartSession
{
    public record StartSessionCommand(Guid UserId, Guid PlanExerciseItemId) : IRequest<Result<StartSessionDto>>;
}
