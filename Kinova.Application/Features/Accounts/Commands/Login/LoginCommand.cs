using Kinova.Application.Common.DTOs.AccountDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Application.Features.Accounts.Commands.Login
{
    public sealed record LoginCommand(string Email, string Password) : IRequest<Result<AuthResponseDto>>
    {
    }
}
