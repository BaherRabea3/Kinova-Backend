using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Patients.Queries.GetActivePlans
{
    public sealed record GetActivePlansQuery(Guid UserId) : IRequest<Result<List<PlanResponseDto>>>
    {
    }
}
