namespace Kinova.API.Requests.Doctor
{
    public class UpdateDoctorProfileRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? LicenseNumber { get; set; }
        public string? Specialization { get; set; }
    }
}
