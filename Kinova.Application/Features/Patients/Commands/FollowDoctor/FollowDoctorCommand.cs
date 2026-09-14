using Kinova.Application.Common.DTOs.PatientDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Patients.Commands.FollowDoctor
{
    public sealed record FollowDoctorCommand(Guid UserId, Guid DoctorId) : IRequest<Result<PatientDetailsDto>>
    {
    }
}
