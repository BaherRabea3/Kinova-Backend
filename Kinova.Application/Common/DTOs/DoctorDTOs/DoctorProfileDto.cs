namespace Kinova.Application.Common.DTOs.DoctorDTOs
{
    public class DoctorProfileDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? LicenseNumber { get; set; }
        public string? Specialization { get; set; }
        public int PatientsCount { get; set; }
    }
}
