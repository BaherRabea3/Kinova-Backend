
namespace Kinova.Application.Common.DTOs.DoctorDTOs
{
    public class DoctorSummaryDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Specialization { get; set; }
        public string? LicenseNumber { get; set; }
        public int PatientsCount { get; set; }
    }
}
