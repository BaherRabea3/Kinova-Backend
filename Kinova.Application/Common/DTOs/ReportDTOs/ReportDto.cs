
using Kinova.Application.Common.DTOs.MovementErrorDTOs;

namespace Kinova.Application.Common.DTOs.ReportDTOs
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SummaryText { get; set; } = string.Empty;
        public decimal OverallScore { get; set; }
        public int TotalRepetitions { get; set; }
        public int CorrectRepetitions { get; set; }
        public double AccuracyPercentage { get; set; }
        public double AverageRangeOfMotion { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public List<MovementErrorSummaryDto> Errors { get; set; } = new List<MovementErrorSummaryDto>();
    }
}
