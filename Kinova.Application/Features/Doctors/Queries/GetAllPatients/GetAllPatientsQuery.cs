using Kinova.Application.Common.DTOs.CommonDTOs;
using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Queries.GetAllPatients
{
   
    public sealed record GetAllPatientsQuery(
        int Page = 1,
        int PageSize = 10,
        string? Search = null,
        string? Status = null) : IRequest<Result<PagedResult<AllPatientsListItemDto>>>
    {
    }
}
