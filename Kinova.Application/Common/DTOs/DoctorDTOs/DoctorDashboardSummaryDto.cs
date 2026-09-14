namespace Kinova.Application.Common.DTOs.DoctorDTOs
{
    public class DoctorDashboardSummaryDto
    {
        public int TotalPatients { get; set; }
        public int ActivePatients { get; set; }
        public int InactivePatients { get; set; }

        /// <summary>
        /// Sessions currently being performed by the doctor's patients right now.
        /// The MVP has no session scheduling (see Post-MVP scope), so this stands in for "upcoming/live" sessions.
        /// </summary>
        public int SessionsInProgress { get; set; }

        public int TotalReports { get; set; }

        /// <summary>
        /// Completed sessions that don't have a generated report yet.
        /// </summary>
        public int PendingReports { get; set; }
    }
}
