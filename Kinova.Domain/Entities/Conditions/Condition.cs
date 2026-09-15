using Kinova.Domain.Entities.Patients;


namespace Kinova.Domain.Entities.Conditions
{
    public class Condition
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
