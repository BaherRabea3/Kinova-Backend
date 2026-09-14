
namespace Kinova.Application.Common.DTOs.SessionDTOs
{
    public class StartSessionDto
    {
        public Guid Id { get; set; }
        public DateOnly SessionDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }   
        public string Status { get; set; }
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public Guid? PlanExerciseItemId { get; set; }
        public int? TargetReps { get; set; }
        public int? TargetSets { get; set; }
        public Guid? ReportId { get; set; }
    }
}
