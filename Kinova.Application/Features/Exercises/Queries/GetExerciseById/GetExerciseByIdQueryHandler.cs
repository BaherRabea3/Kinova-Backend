
using Kinova.Application.Common.DTOs.ExerciseDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Exercises;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Exercises.Queries.GetExerciseById
{
    public sealed class GetExerciseByIdQueryHandler : IRequestHandler<GetExerciseByIdQuery, Result<ExerciseDetailsResponseDto>>
    {
        private readonly IKinovaDbContext _context;

        public GetExerciseByIdQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ExerciseDetailsResponseDto>> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
        {
            var exerciseResponse = await _context.Exercises
                .Select(x => new ExerciseDetailsResponseDto
                {
                    Id = x.Id,
                    BodyPart = x.BodyPart,
                    Category = x.Category,
                    DifficultyLevel = x.DifficultyLevel,
                    Name = x.Name,
                    VideoUrl = x.VideoUrl,
                    Instructions = x.Instructions,
                    TargetJoints = x.TargetJoints,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (exerciseResponse is null)
                return Result.Failure<ExerciseDetailsResponseDto>(ExerciseErrors.NotFound);

            return Result.Success(exerciseResponse);
        }
    }
}
