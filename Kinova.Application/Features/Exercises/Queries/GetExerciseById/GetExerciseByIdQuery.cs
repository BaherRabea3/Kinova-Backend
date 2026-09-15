
using Kinova.Application.Common.DTOs.ExerciseDTOs;
using Kinova.Domain.Common;
using MediatR;

namespace Kinova.Application.Features.Exercises.Queries.GetExerciseById
{
    public sealed record GetExerciseByIdQuery(Guid Id) : IRequest<Result<ExerciseDetailsResponseDto>>
    {
    }
}
