using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Queries.GetDashboardSummary
{
    public sealed record GetDashboardSummaryQuery(Guid DoctorUserId) : IRequest<Result<DoctorDashboardSummaryDto>>
    {
    }
}
