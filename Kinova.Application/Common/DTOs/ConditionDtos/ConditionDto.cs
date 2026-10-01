

namespace Kinova.Application.Common.DTOs.ConditionDtos
{
    public class ConditionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
