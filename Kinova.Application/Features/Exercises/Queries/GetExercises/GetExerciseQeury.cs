using Kinova.Application.Common.DTOs.ExerciseDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Exercises.Queries.GetExercises
{
    public sealed record GetExerciseQeury(
        string? Search = null,
        string? Category = null,
        string? BodyPart = null,
        string? DifficultyLevel = null) : IRequest<Result<List<ExercisesResponseDto>>>
    {
    }
}
