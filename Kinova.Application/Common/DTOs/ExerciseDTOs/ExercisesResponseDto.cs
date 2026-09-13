
namespace Kinova.Application.Common.DTOs.ExerciseDTOs
{
    public class ExercisesResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string BodyPart { get; set; } = null!;
        public string? DifficultyLevel { get; set; }
    }

}
