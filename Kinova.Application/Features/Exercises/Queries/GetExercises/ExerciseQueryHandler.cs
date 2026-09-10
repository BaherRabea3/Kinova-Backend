using Kinova.Application.Common.DTOs.ExerciseDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Exercises.Queries.GetExercises
{
    public sealed class GetExerciseQueryHandler : IRequestHandler<GetExerciseQeury, Result<List<ExercisesResponseDto>>>
    {
        private readonly IKinovaDbContext _context;

        public GetExerciseQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ExercisesResponseDto>>> Handle(GetExerciseQeury request, CancellationToken cancellationToken)
        {
            var response = await _context.Exercises
                .Select(x => new ExercisesResponseDto
                {
                    Id = x.Id,
                    BodyPart = x.BodyPart,
                    Category = x.Category,
                    Name = x.Name,
                }).ToListAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
