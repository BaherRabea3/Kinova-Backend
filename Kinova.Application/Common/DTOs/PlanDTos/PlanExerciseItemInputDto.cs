namespace Kinova.Application.Common.DTOs.PlanDTos
{
    public class PlanExerciseItemInputDto
    {
        public Guid ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public int FrequencyPerWeek { get; set; }
    }
}
