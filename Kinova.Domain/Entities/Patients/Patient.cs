using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.Plans;
using Kinova.Domain.Entities.Reports;
using Kinova.Domain.Entities.Sessions;

namespace Kinova.Domain.Entities.Patients
{
    public class Patient
    {
        public Guid Id { get; set; }                    
        public Guid UserId { get; set; }               
        public Sex Gender { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string? CarePath { get; set; }
        public Guid DoctorId { get; set; }             

        public Doctor? Doctor { get; set; }
        public ICollection<Plan> Plans { get; set; }  = new List<Plan>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();   
    }
}
