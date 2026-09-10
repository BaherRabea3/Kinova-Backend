
namespace Kinova.Application.Common.DTOs.PlanDTos
{
    public class PlanResponseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Source { get; set; } = string.Empty; 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } 

        public Guid DoctorId { get; set; }
        public string? DoctorName { get; set; }   

        public List<PlanExerciseItemDto> Exercises { get; set; } = new();
    }

    public class PlanExerciseItemDto
    {
        public Guid Id { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public int FrequencyPerWeek { get; set; }

        public ExerciseSummaryDto Exercise { get; set; } = null!;
    }

    public class ExerciseSummaryDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? BodyPart { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? VideoUrl { get; set; }
    }
}
