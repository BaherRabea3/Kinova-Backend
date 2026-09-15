
using Kinova.Domain.Entities.Patients;

namespace Kinova.Application.Common.DTOs.PatientDTOs
{
    public class PatientDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Sex Gender { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string? CarePath { get; set; }
        public Guid? DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
    }
}
