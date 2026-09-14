using Kinova.Domain.Entities.Patients;

namespace Kinova.Application.Common.DTOs.DoctorDTOs
{
    public class DoctorPatientListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Sex Gender { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string? CarePath { get; set; }

        /// <summary>
        /// "Active" when the patient currently has at least one active plan, otherwise "Inactive".
        /// </summary>
        public string Status { get; set; } = string.Empty;

        public string? ActivePlanName { get; set; }
        public int TotalSessions { get; set; }
        public DateOnly? LastSessionDate { get; set; }
    }
}
