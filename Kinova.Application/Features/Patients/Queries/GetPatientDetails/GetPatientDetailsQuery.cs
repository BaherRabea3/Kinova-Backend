
using Kinova.Application.Common.DTOs.PatientDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Patients.Queries.GetPatientDetails
{
    public sealed record GetPatientDetailsQuery(Guid UserId) : IRequest<Result<PatientDetailsDto>>
    {
    }
}
