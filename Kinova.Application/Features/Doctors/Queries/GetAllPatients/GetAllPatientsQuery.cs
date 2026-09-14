using Kinova.Application.Common.DTOs.CommonDTOs;
using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Queries.GetAllPatients
{
    /// <summary>
    /// Returns every patient in the system, regardless of which doctor (if any) they're assigned to.
    /// Status accepts "Active"/"Inactive" (based on whether the patient has a currently active plan).
    /// </summary>
    public sealed record GetAllPatientsQuery(
        int Page = 1,
        int PageSize = 10,
        string? Search = null,
        string? Status = null) : IRequest<Result<PagedResult<AllPatientsListItemDto>>>
    {
    }
}
