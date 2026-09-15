using Kinova.Application.Common.DTOs.AccountDTOs;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Patients;
using MediatR;

namespace Kinova.Application.Features.Accounts.Commands.Register.Patient
{
    public sealed record PatientRegisterCommand
        (string FirstName,
        string LastName,
        string Email,
        DateTime DateOfBirth,
        Sex? Gender,
        decimal? Height,
        decimal? Weight,
        string Password,
        string ConfirmPassword) : IRequest<Result<AuthResponseDto>>
    {
    }
}
