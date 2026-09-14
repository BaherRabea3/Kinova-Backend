using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Patients.Queries.GetAllDoctors
{
    public sealed record GetDoctorsQuery(
       string? Search = null,
       string? Specialization = null) : IRequest<Result<List<DoctorSummaryDto>>>
    {
    }
}
