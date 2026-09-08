using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Accounts.Commands.Logout
{
    public sealed record LogoutCommand(string Email) : IRequest<Result>
    {
    }
}
