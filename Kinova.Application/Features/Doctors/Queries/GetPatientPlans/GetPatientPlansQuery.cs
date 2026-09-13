using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Queries.GetPatientPlans
{
    public sealed record GetPatientPlansQuery(Guid DoctorUserId, Guid PatientId) : IRequest<Result<List<PlanResponseDto>>>
    {
    }
}
