namespace Kinova.API.Requests.Doctor
{
    public class CreatePlanRequest
    {
        public Guid PatientId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public List<CreatePlanExerciseItemRequest> Exercises { get; set; } = new();
    }

    public class CreatePlanExerciseItemRequest
    {
        public Guid ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public int FrequencyPerWeek { get; set; }
    }
}
