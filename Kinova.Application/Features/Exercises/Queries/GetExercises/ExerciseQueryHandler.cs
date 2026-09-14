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
            var query = _context.Exercises.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(x => x.Name!.Contains(request.Search));

            if (!string.IsNullOrWhiteSpace(request.Category))
                query = query.Where(x => x.Category == request.Category);

            if (!string.IsNullOrWhiteSpace(request.BodyPart))
                query = query.Where(x => x.BodyPart == request.BodyPart);

            if (!string.IsNullOrWhiteSpace(request.DifficultyLevel))
                query = query.Where(x => x.DifficultyLevel == request.DifficultyLevel);

            var response = await query
                .OrderBy(x => x.Name)
                .Select(x => new ExercisesResponseDto
                {
                    Id = x.Id,
                    BodyPart = x.BodyPart,
                    Category = x.Category,
                    Name = x.Name,
                    DifficultyLevel = x.DifficultyLevel
                }).ToListAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
