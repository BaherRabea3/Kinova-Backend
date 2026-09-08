using Kinova.Application.Common.DTOs.AccountDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Accounts.Commands.GenerateJwtToken
{
    public sealed record GenerateJwtTokenCommand(string Token, string RefreshToken) : IRequest<Result<AuthResponseDto>>
    {
    }
}
