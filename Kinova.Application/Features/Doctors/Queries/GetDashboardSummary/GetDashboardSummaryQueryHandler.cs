using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.Sessions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Doctors.Queries.GetDashboardSummary
{
    public sealed class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, Result<DoctorDashboardSummaryDto>>
    {
        private readonly IKinovaDbContext _context;

        public GetDashboardSummaryQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<DoctorDashboardSummaryDto>> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.UserId == request.DoctorUserId, cancellationToken);

            if (doctor is null)
                return Result.Failure<DoctorDashboardSummaryDto>(DoctorErrors.UnAuthorized());

            var totalPatients = await _context.Patients
                .AsNoTracking()
                .CountAsync(p => p.DoctorId == doctor.Id, cancellationToken);

            var activePatients = await _context.Patients
                .AsNoTracking()
                .CountAsync(p => p.DoctorId == doctor.Id && p.Plans.Any(pl => pl.IsActive), cancellationToken);

            var sessionsInProgress = await _context.Sessions
                .AsNoTracking()
                .CountAsync(s => s.Patient.DoctorId == doctor.Id && s.Status == SessionStatus.InProgress, cancellationToken);

            var totalReports = await _context.Reports
                .AsNoTracking()
                .CountAsync(r => r.DoctorId == doctor.Id, cancellationToken);

            var pendingReports = await _context.Sessions
                .AsNoTracking()
                .CountAsync(s => s.Patient.DoctorId == doctor.Id
                               && s.Status == SessionStatus.Completed
                               && s.Report == null, cancellationToken);

            return Result.Success(new DoctorDashboardSummaryDto
            {
                TotalPatients = totalPatients,
                ActivePatients = activePatients,
                InactivePatients = totalPatients - activePatients,
                SessionsInProgress = sessionsInProgress,
                TotalReports = totalReports,
                PendingReports = pendingReports
            });
        }
    }
}
