using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.Patients;
using Kinova.Domain.Entities.Sessions;

namespace Kinova.Domain.Entities.Reports
{
    public class Report
    {
        public Guid Id { get; set; }                    
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SummaryText { get; set; } = null!;
        public decimal OverallScore { get; set; }
        public int TotalRepetitions { get; set; }
        public int CorrectRepetitions { get; set; }
        public double AverageRangeOfMotion { get; set; }

        public Guid SessionId { get; set; }
        public Guid PatientId { get; set; }             
        public Guid DoctorId { get; set; }              

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Session? Session { get; set; }
    }
}
