using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Commands.UpdateDoctorProfile
{
    public sealed record UpdateDoctorProfileCommand(
        Guid UserId,
        string Email,
        string Name,
        string? LicenseNumber,
        string? Specialization) : IRequest<Result<DoctorProfileDto>>
    {
    }
}
