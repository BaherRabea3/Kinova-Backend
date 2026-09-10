
namespace Kinova.Application.Common.DTOs.ExerciseDTOs
{
    public class ExerciseDetailsResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string BodyPart { get; set; } = null!;
        public string TargetJoints { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public string DifficultyLevel { get; set; } = null!;
    }

}
