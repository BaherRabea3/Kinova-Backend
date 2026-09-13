using Kinova.Application.Common.DTOs.PlanDTos;
using Kinova.Domain.Entities.Patients;

namespace Kinova.Application.Common.DTOs.DoctorDTOs
{
    public class DoctorPatientDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Sex Gender { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string? CarePath { get; set; }
        public string Status { get; set; } = string.Empty;

      
        public List<PlanResponseDto> Plans { get; set; } = new();
        public List<PatientSessionHistoryDto> RecentSessions { get; set; } = new();
    }

    public class PatientSessionHistoryDto
    {
        public Guid Id { get; set; }
        public DateOnly SessionDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ExerciseName { get; set; }
        public decimal? OverallScore { get; set; }
        public int? ValidRepetitions { get; set; }
        public int? InvalidRepetitions { get; set; }
    }
}
