using Kinova.Domain.Entities.PlanExerciseItems;
using Kinova.Domain.Entities.Sessions;
using static System.Collections.Specialized.BitVector32;

namespace Kinova.Domain.Entities.Exercises
{
    public class Exercise
    {
        public Guid Id { get; set; }                   
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string?  BodyPart { get; set; }
        public string? TargetJoints { get; set; }
        public string? Instructions { get; set; }
        public string? VideoUrl { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? Defaults { get; set; }

        public ICollection<PlanExerciseItem> PlanExercises { get; set; } = new List<PlanExerciseItem>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();          
    }
}
