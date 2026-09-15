using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Commands.UpdatePlan
{
    public sealed record UpdatePlanCommand(
        Guid DoctorUserId,
        Guid PlanId,
        string? Name,
        string? Description,
        DateTime StartDate,
        DateTime EndDate,
        bool IsActive) : IRequest<Result<PlanResponseDto>>
    {
    }
}
