namespace Kinova.Application.Common.DTOs.DoctorDTOs
{
    public class DoctorDashboardSummaryDto
    {
        public int TotalPatients { get; set; }
        public int ActivePatients { get; set; }
        public int InactivePatients { get; set; }

      
        public int SessionsInProgress { get; set; }

        public int TotalReports { get; set; }

       
        public int PendingReports { get; set; }
    }
}
