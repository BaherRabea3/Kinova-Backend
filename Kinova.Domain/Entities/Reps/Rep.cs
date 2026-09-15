using Kinova.Domain.Entities.JointAngles;
using Kinova.Domain.Entities.MovementErrors;
using Kinova.Domain.Entities.Sessions;

namespace Kinova.Domain.Entities.Reps
{
    public class Rep
    {
        public Guid Id { get; set; }                    
        public int RepNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCorrect { get; set; }
        public Guid SessionId { get; set; }             

        public Session? Session { get; set; }
        public ICollection<JointAngleReading> AngleReadings { get; set; } = new List<JointAngleReading>();
        public ICollection<MovementError> MovementErrors { get; set; }  = new List<MovementError>();            
    }
}
