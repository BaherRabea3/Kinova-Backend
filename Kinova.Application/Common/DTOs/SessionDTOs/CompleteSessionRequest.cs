using Kinova.Application.Common.DTOs.RepDTOs;

namespace Kinova.Application.Common.DTOs.SessionDTOs
{
    public record CompleteSessionRequest
    {
        public List<RepUploadDto> Reps { get; set; } = new List<RepUploadDto>();
    }
}
