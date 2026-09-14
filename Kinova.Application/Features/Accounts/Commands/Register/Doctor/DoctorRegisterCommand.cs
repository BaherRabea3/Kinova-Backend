using Kinova.Application.Common.DTOs.AccountDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Accounts.Commands.Register.Doctor
{
    public sealed record DoctorRegisterCommand
        (string FirstName,
        string LastName,
        string Email,
        DateTime DateOfBirth,
        string? LicenseNumber,
        string? Specialization,
        string Password,
        string ConfirmPassword) : IRequest<Result<AuthResponseDto>>
    {
    }
}
