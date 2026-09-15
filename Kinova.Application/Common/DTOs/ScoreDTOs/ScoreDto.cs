
namespace Kinova.Application.Common.DTOs.ScoreDTOs
{
    public class ScoreDto
    {
        public decimal AccuracyScore { get; set; }
        public decimal RangeOfMotionScore {  get; set; }
        public decimal StabilityScore { get; set; }
        public decimal OverallScore { get; set; }
        public int ValidRepetitions { get; set; }
        public int InvalidRepetitions { get; set; }

    }
}
