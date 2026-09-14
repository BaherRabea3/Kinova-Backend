using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Queries.GetDoctorProfile
{
    public sealed record GetDoctorProfileQuery(Guid UserId, string Email) : IRequest<Result<DoctorProfileDto>>
    {
    }
}
