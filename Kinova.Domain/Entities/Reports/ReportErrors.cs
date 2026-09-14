using Kinova.Domain.Common;

namespace Kinova.Domain.Entities.Reports
{
    public static class ReportErrors
    {
        public static Error NotFound()
            => Error.NotFound("Report.NotFound", "Report not found");
    }
}
