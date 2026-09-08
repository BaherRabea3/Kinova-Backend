using Kinova.Domain.Entities.Reps;

namespace Kinova.Domain.Entities.MovementErrors
{
    public class MovementError
    {
        public Guid Id { get; set; }                    
        public string? ErrorType { get; set; }          
        public string? BodyPart { get; set; }
        public ErrorSeverity Severity { get; set; }
        public decimal DeviationValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid RepId { get; set; }                 

        public Rep? Rep { get; set; }
    }
}
