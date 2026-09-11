using Kinova.Application.Common.DTOs.JointAngleReadingDTOs;
using Kinova.Application.Common.DTOs.MovementErrorDTOs;
using Kinova.Application.Common.DTOs.ScoreDTOs;

namespace Kinova.Application.Common.DTOs.RepDTOs
{
    public class RepUploadDto
    {
        public int RepNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCorrect { get; set; }
        public List<JointAngleReadingDto> JointAngleReadings { get; set; } = new List<JointAngleReadingDto>();
        public List<MovementErrorDTO> MovementErrors { get; set; } = new List<MovementErrorDTO>();
    }
        

}
