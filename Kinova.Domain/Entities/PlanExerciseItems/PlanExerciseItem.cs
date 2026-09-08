using Kinova.Domain.Entities.Exercises;
using Kinova.Domain.Entities.Plans;
using Kinova.Domain.Entities.Sessions;

namespace Kinova.Domain.Entities.PlanExerciseItems
{
    public class PlanExerciseItem
    {
        public Guid Id { get; set; }                    
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public int FrequencyPerWeek { get; set; }
        public Guid PlanId { get; set; }                
        public Guid ExerciseId { get; set; }            

        public Plan? Plan { get; set; }
        public Exercise? Exercise { get; set; }
        public ICollection<Session> Sessions { get; set; }  = new List<Session>();
    }
}
