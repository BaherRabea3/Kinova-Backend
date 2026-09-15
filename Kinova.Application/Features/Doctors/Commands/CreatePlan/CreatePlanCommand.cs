using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Commands.CreatePlan
{
    public sealed record CreatePlanCommand(
        Guid DoctorUserId,
        Guid PatientId,
        string? Name,
        string? Description,
        DateTime StartDate,
        DateTime EndDate,
        bool IsActive,
        List<PlanExerciseItemInputDto> Exercises) : IRequest<Result<PlanResponseDto>>
    {
    }
}
