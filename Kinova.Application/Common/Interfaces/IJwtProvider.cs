using Kinova.Application.Common.DTOs.AccountDTOs;
using System.Security.Claims;

namespace Kinova.Application.Common.Interfaces
{
    public interface IJwtProvider
    {
        Task<AuthResponseDto> GenerateTokenAsync(string email);

        ClaimsPrincipal? GetPrincipaleFromJwtToken(string? jwtToken);
    }
}
