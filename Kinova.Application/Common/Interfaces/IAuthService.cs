using Kinova.Application.Common.DTOs.AccountDTOs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kinova.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(
            string FirstName,
            string LastName,
            string Email,
            string Password,
            string Role);

        Task<AuthResponseDto> LoginAsync(
           string Email,
           string Password);

        Task LogoutAsync(string Email);

        Task<AuthResponseDto> GenerateNewJwtToken(string Email, string RefreshToken);
    }
}
