using Kinova.Domain.Entities.Patients;

namespace Kinova.Application.Common.DTOs.DoctorDTOs
{
    public class AllPatientsListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Sex Gender { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string? CarePath { get; set; }

        public string Status { get; set; } = string.Empty;

        public Guid? DoctorId { get; set; }

        public string? DoctorName { get; set; }

        public int TotalSessions { get; set; }
        public DateOnly? LastSessionDate { get; set; }
    }
}
