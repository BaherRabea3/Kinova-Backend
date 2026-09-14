using Kinova.Application.Common.DTOs.MovementErrorDTOs;
using Kinova.Application.Common.DTOs.RepDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Entities.Sessions;
using System.Text.Json;

namespace Kinova.Application.Common.Helpers
{
    public class ReportContentBuilder : IReportContentBuilder
    {
        public (string, string) Build(Session session, List<RepUploadDto> reps)
        {
            var grouped = reps.SelectMany(r => r.MovementErrors)
                .GroupBy(e => new { e.ErrorType, e.BodyPart })
                .Select(g => new MovementErrorSummaryDto
                {
                    ErrorType = g.Key.ErrorType,
                    BodyPart = g.Key.BodyPart,
                    Occurrences = g.Count(),
                    MostFrequentSeverity = g.GroupBy(e => e.Severity).OrderByDescending(s => s.Count()).First().Key.ToString()
                }
                )
                .ToList();

            var summary = $"Patient completed {reps.Count} repetitions " +
                           $"({reps.Count(r => r.IsCorrect)} valid). " +
                           (grouped.Any()
                               ? $"Recurring issues: {string.Join(", ", grouped.Select(g => $"{g.ErrorType} ({g.BodyPart})"))}."
                               : "No recurring movement errors detected.");

            return (summary, JsonSerializer.Serialize(grouped));
        }
    }
}