
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Sessions.Commands.CancelSession
{
    public record CancelSessionCommand(Guid SessionId, Guid UserId) : IRequest<Result<Unit>>
    {
    }
}
