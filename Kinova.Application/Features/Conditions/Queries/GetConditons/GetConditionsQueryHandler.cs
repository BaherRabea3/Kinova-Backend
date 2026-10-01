using Kinova.Application.Common.DTOs.ConditionDtos;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Conditions.Queries.GetConditons
{
    public sealed class GetConditionsQueryHandler : IRequestHandler<GetConditionsQuery, Result<List<ConditionDto>>>
    {
        private readonly IKinovaDbContext _context;

        public GetConditionsQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ConditionDto>>> Handle(GetConditionsQuery request, CancellationToken cancellationToken)
        {
            var conditions = await _context.Conditions
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new ConditionDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync(cancellationToken);

            return Result.Success(conditions);
        }
    }
}