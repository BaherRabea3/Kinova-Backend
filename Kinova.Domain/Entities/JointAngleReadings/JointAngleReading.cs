using Kinova.Domain.Entities.Reps;


namespace Kinova.Domain.Entities.JointAngles
{
    public class JointAngleReading
    {
        public long Id { get; set; }                    
        public string? JointName { get; set; }           
        public decimal Angle { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid RepId { get; set; }                 

        public Rep? Rep { get; set; }
    }
}
