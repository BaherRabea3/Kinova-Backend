using Kinova.Application.Common.DTOs.MovementErrorDTOs;
using Kinova.Application.Common.DTOs.ReportDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Kinova.Application.Features.Doctors.Queries.GetPatientReports
{
    public sealed class GetPatientReportsQueryHandler : IRequestHandler<GetPatientReportsQuery, Result<List<ReportDto>>>
    {
        private readonly IKinovaDbContext _context;

        public GetPatientReportsQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ReportDto>>> Handle(GetPatientReportsQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.UserId == request.DoctorUserId, cancellationToken);

            if (doctor is null)
                return Result.Failure<List<ReportDto>>(DoctorErrors.UnAuthorized());

            var patient = await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);

            if (patient is null)
                return Result.Failure<List<ReportDto>>(DoctorErrors.PatientNotFound());

            if (patient.DoctorId != doctor.Id)
                return Result.Failure<List<ReportDto>>(DoctorErrors.NotYourPatient());

            var reports = await _context.Reports
                .AsNoTracking()
                .Include(r => r.Score)
                .Include(r => r.Session)
                    .ThenInclude(s => s.Exercise)
                .Where(r => r.PatientId == patient.Id)
                .OrderByDescending(r => r.EndDate)
                .ToListAsync(cancellationToken);

            var result = reports.Select(r =>
            {
                var totalReps = (r.Score?.ValidRepetitions ?? 0) + (r.Score?.InvalidRepetitions ?? 0);
                var correctReps = r.Score?.ValidRepetitions ?? 0;

                List<MovementErrorSummaryDto> errors;
                try
                {
                    errors = JsonSerializer.Deserialize<List<MovementErrorSummaryDto>>(r.ErrorsJson) ?? new();
                }
                catch
                {
                    errors = new();
                }

                return new ReportDto
                {
                    Id = r.Id,
                    SessionId = r.SessionId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    SummaryText = r.SummaryText,
                    OverallScore = r.Score?.OverallScore ?? 0,
                    TotalRepetitions = totalReps,
                    CorrectRepetitions = correctReps,
                    AccuracyPercentage = totalReps == 0 ? 0 : Math.Round(correctReps * 100.0 / totalReps, 2),
                    AverageRangeOfMotion = (double)(r.Score?.RangeOfMotionScore ?? 0),
                    ExerciseName = r.Session?.Exercise?.Name ?? string.Empty,
                    PatientName = patient.Name,
                    Errors = errors
                };
            }).ToList();

            return Result.Success(result);
        }
    }
}
