using Kinova.Domain.Entities.Patients;
using Kinova.Domain.Entities.Plans;
using Kinova.Domain.Entities.Reports;

namespace Kinova.Domain.Entities.Doctors
{
    public class Doctor
    {
        public Guid Id { get; set; }                    
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Specialization { get; set; }

        public ICollection<Patient> Patients { get; set; }  = new List<Patient>();
        public ICollection<Plan> Plans { get; set; }   = new List<Plan>();      
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
