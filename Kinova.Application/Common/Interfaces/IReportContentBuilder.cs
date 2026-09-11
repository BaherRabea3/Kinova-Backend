using Kinova.Application.Common.DTOs.RepDTOs;
using Kinova.Domain.Entities.Sessions;

namespace Kinova.Application.Common.Interfaces
{
    public interface IReportContentBuilder
    {
        (string SummaryText, string ErrorsJson) Build(Session session, List<RepUploadDto> reps);
    }
}
