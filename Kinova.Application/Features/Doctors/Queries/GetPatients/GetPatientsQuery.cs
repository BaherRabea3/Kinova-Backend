using Kinova.Application.Common.DTOs.CommonDTOs;
using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Queries.GetPatients
{
    /// <summary>
    /// Status accepts "Active" (patient has a currently active plan) or "Inactive" (no active plan).
    /// Leave null/empty to return patients regardless of status.
    /// </summary>
    public sealed record GetPatientsQuery(
        Guid DoctorUserId,
        int Page = 1,
        int PageSize = 10,
        string? Search = null,
        string? Status = null) : IRequest<Result<PagedResult<DoctorPatientListItemDto>>>
    {
    }
}
