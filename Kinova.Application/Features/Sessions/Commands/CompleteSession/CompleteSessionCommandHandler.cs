using Kinova.Application.Common.DTOs.ReportDTOs;
using Kinova.Application.Common.DTOs.ScoreDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.JointAngles;
using Kinova.Domain.Entities.MovementErrors;
using Kinova.Domain.Entities.Patients;
using Kinova.Domain.Entities.Reports;
using Kinova.Domain.Entities.Reps;
using Kinova.Domain.Entities.Scores;
using Kinova.Domain.Entities.Sessions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Sessions.Commands.CompleteSession
{
    public class CompleteSessionHandler : IRequestHandler<CompleteSessionCommand, Result<ScoreDto>>
    {
        private readonly IKinovaDbContext _context;
        private readonly IReportContentBuilder _reportGenerator;
        private readonly IScoreCalculator _socoreCalculator;

        public CompleteSessionHandler(IKinovaDbContext db, IReportContentBuilder reportGenerator, IScoreCalculator socoreCalculator)
        {
            _context = db;
            _reportGenerator = reportGenerator;
            _socoreCalculator = socoreCalculator;
        }

        public async Task<Result<ScoreDto>> Handle(CompleteSessionCommand request, CancellationToken ct)
        {
            var session = await _context.Sessions
                .Include(s => s.Patient)
                .FirstOrDefaultAsync(s => s.Id == request.SessionId, ct);


            if (session is null)
                return Result.Failure<ScoreDto>(SessionErrors.NotFound());

            if (session.Patient.UserId != request.UserId)
                return Result.Failure<ScoreDto>(PatientErrors.NotYourSession());


            // Idempotent retry
            if (session.Status == SessionStatus.Completed)
            {
                var existing = await _context.Scores
                    .AsNoTracking()
                    .FirstAsync(r => r.SessionId == session.Id, ct);

                return Result.Success(MapToResponse(existing));
            }

            if (session.Status == SessionStatus.Cancelled)
                return Result.Failure<ScoreDto>(SessionErrors.CannotCompleted());

            await using var tx = await _context.Database.BeginTransactionAsync(ct);

            foreach (var repDto in request.Payload.Reps)
            {
                var rep = new Rep
                {
                    Id = Guid.NewGuid(),
                    SessionId = session.Id,
                    RepNumber = repDto.RepNumber,
                    StartTime = repDto.StartTime,
                    EndTime = repDto.EndTime,
                    IsCorrect = repDto.IsCorrect
                };
                _context.Reps.Add(rep);

                foreach (var j in repDto.JointAngleReadings)
                    _context.JointAngleReadings.Add(new JointAngleReading
                    {
                        RepId = rep.Id,
                        JointName = j.JointName,
                        Angle = j.Angle,
                        Timestamp = j.Timestamp
                    });

                foreach (var e in repDto.MovementErrors)
                    _context.MovementErrors.Add(new MovementError
                    {
                        RepId = rep.Id,
                        ErrorType = e.ErrorType,
                        BodyPart = e.BodyPart,
                        Severity = e.Severity,
                        DeviationValue = e.DeviationValue,
                        CreatedAt = DateTime.UtcNow
                    });
            }

            var score = _socoreCalculator.Calculate(request.Payload.Reps);
            score.SessionId = session.Id;
            _context.Scores.Add(score);

            if(session.Patient.DoctorId is not null)
            {
                var (summary, errorsJson) = _reportGenerator.Build(session, request.Payload.Reps);
                var report = new Report
                {
                    SessionId = session.Id,
                    ScoreId = score.Id,
                    PatientId = session.PatientId,
                    DoctorId = session.Patient.DoctorId.Value,
                    StartDate = session.StartTime,
                    EndDate = DateTime.UtcNow,
                    SummaryText = summary,
                    ErrorsJson = errorsJson
                };
                _context.Reports.Add(report);
            }
            
            session.EndTime = DateTime.UtcNow;
            session.Status = SessionStatus.Completed;

            await _context.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);

            return Result.Success(MapToResponse(score));
        }
        private ScoreDto MapToResponse(Score score)
            => new ScoreDto
            {
                ValidRepetitions = score.ValidRepetitions,
                InvalidRepetitions = score.InvalidRepetitions,
                AccuracyScore = score.AccuracyScore,
                RangeOfMotionScore = score.RangeOfMotionScore,
                StabilityScore = score.StabilityScore,
                OverallScore = score.OverallScore
            };
    }
}
