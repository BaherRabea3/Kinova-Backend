using Kinova.Domain.Entities.Sessions;

namespace Kinova.Domain.Entities.Scores
{
    public class Score
    {
        public Guid Id { get; set; }                    
        public decimal AccuracyScore { get; set; }
        public decimal RangeOfMotionScore { get; set; }             
        public decimal StabilityScore { get; set; }
        public decimal OverallScore { get; set; }
        public int ValidRepetitions { get; set; }
        public int InvalidRepetitions { get; set; }
        public Guid SessionId { get; set; }             

        public Session? Session { get; set; }
    }
}
