using Kinova.Application.Common.DTOs.PatientDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Conditions.Commands.ChooseCondition
{
    public sealed record ChooseConditionCommand(Guid UserId, Guid ConditionId) : IRequest<Result<PatientDetailsDto>>
    {
    }
}