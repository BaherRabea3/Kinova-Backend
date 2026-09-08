using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.Patients;
using Kinova.Domain.Entities.PlanExerciseItems;

namespace Kinova.Domain.Entities.Plans
{
    public class Plan
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public PlanSource Source { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public Guid DoctorId { get; set; }              
        public Guid PatientId { get; set; }             

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
        public ICollection<PlanExerciseItem> Exercises { get; set; } = new List<PlanExerciseItem>();
    }
}
