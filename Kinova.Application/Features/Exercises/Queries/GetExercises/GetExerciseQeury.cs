using Kinova.Application.Common.DTOs.ExerciseDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Exercises.Queries.GetExercises
{
    public sealed record GetExerciseQeury() : IRequest<Result<List<ExercisesResponseDto>>>
    {
    }
}
