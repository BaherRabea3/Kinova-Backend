using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.Patients;
using Microsoft.AspNetCore.Identity;

namespace Kinova.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }

        public string? RefreshToken { get; set; } = default!;
        public DateTime? RefreshTokenExpiration { get; set; } = default!;

        public Doctor? Doctor { get; set; }              
        public Patient? Patient { get; set; }
    }
}
