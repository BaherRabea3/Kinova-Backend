using Kinova.Domain.Entities.Patients;

namespace Kinova.API.Requests.Account
{
    public class DoctorRegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? LicenseNumber {  get; set; } = string.Empty;
        public string? Specialization {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmationPassword { get; set; } = string.Empty;

    }
}
