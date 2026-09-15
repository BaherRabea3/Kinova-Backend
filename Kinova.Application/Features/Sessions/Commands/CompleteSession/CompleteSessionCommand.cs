
using Kinova.Application.Common.DTOs.ReportDTOs;
using Kinova.Application.Common.DTOs.ScoreDTOs;
using Kinova.Application.Common.DTOs.SessionDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Sessions.Commands.CompleteSession
{
    public record CompleteSessionCommand(Guid SessionId, Guid UserId, CompleteSessionRequest Payload) : IRequest<Result<ScoreDto>>
    {
    }
}
