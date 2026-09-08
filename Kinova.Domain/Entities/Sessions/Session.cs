using Kinova.Domain.Entities.Exercises;
using Kinova.Domain.Entities.Patients;
using Kinova.Domain.Entities.PlanExerciseItems;
using Kinova.Domain.Entities.Reports;
using Kinova.Domain.Entities.Reps;
using Kinova.Domain.Entities.Scores;

namespace Kinova.Domain.Entities.Sessions
{
    public class Session
    {
        public Guid Id { get; set; }

        public DateOnly SessionDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public SessionStatus Status { get; set; }
        public int CorrectReps { get; set; }

        public Guid PatientId { get; set; }
        public Guid ExerciseId { get; set; }
        public Guid? ItemId { get; set; }             

        public Patient Patient { get; set; } = null!;
        public Exercise Exercise { get; set; } = null!;
        public PlanExerciseItem? PlanExerciseItem { get; set; }
        public ICollection<Rep> Reps { get; set; } = new List<Rep>();
        public Report? Report { get; set; }
        public Score? Score { get; set; }
    }

}
