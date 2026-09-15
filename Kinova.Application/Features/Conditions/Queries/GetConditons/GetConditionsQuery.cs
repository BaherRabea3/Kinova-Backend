using Kinova.Application.Common.DTOs.ConditionDtos;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Conditions.Queries.GetConditons
{
    public sealed record GetConditionsQuery : IRequest<Result<List<ConditionDto>>>
    {
    }
}
