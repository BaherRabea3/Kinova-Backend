namespace Kinova.Application.Common.DTOs.MovementErrorDTOs
{
    public class MovementErrorSummaryDto
    {
        public string ErrorType { get; set; } = string.Empty;
        public string BodyPart { get; set; } = string.Empty;
        public int Occurrences { get; set; }
        public string MostFrequentSeverity { get; set; } = string.Empty;
    }
}
