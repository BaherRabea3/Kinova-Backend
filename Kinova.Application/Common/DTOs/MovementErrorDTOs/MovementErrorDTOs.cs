
using Kinova.Domain.Entities.MovementErrors;

namespace Kinova.Application.Common.DTOs.MovementErrorDTOs
{
    public class MovementErrorDTO
    {
        public string ErrorType { get; set; } = string.Empty;
        public string BodyPart { get; set; } = string.Empty;
        public ErrorSeverity Severity { get; set; }
        public decimal DeviationValue { get; set; }

    }
}
