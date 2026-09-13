using Kinova.Application.Common.DTOs.ReportDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Doctors.Queries.GetPatientReports
{
    public sealed record GetPatientReportsQuery(Guid DoctorUserId, Guid PatientId) : IRequest<Result<List<ReportDto>>>
    {
    }
}
