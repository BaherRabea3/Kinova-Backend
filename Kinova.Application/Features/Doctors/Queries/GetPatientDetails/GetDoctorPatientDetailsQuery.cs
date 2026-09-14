using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Queries.GetPatientDetails
{
    public sealed record GetDoctorPatientDetailsQuery(Guid DoctorUserId, Guid PatientId) : IRequest<Result<DoctorPatientDetailsDto>>
    {
    }
}
