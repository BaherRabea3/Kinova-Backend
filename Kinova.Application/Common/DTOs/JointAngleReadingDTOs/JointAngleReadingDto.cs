
namespace Kinova.Application.Common.DTOs.JointAngleReadingDTOs
{
    public class JointAngleReadingDto
    {
        public string JointName { get; set; } = string.Empty;
        public decimal Angle { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
