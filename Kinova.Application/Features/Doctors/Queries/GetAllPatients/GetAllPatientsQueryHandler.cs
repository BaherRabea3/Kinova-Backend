using Kinova.Application.Common.DTOs.CommonDTOs;
using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Doctors.Queries.GetAllPatients
{
    public sealed class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, Result<PagedResult<AllPatientsListItemDto>>>
    {
        private const int MaxPageSize = 50;

        private readonly IKinovaDbContext _context;

        public GetAllPatientsQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedResult<AllPatientsListItemDto>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var page = request.Page < 1 ? 1 : request.Page;
            var pageSize = request.PageSize < 1 ? 10 : Math.Min(request.PageSize, MaxPageSize);

            var query = _context.Patients.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(p => p.Name.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                var wantsActive = request.Status.Trim().Equals("Active", StringComparison.OrdinalIgnoreCase);
                query = query.Where(p => p.Plans.Any(pl => pl.IsActive) == wantsActive);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new AllPatientsListItemDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Gender = p.Gender,
                    Height = p.Height,
                    Weight = p.Weight,
                    CarePath = p.CarePath,
                    Status = p.Plans.Any(pl => pl.IsActive) ? "Active" : "Inactive",
                    DoctorId = p.DoctorId,
                    DoctorName = p.Doctor != null ? p.Doctor.Name : null,
                    TotalSessions = p.Sessions.Count,
                    LastSessionDate = p.Sessions.OrderByDescending(s => s.SessionDate)
                                                 .Select(s => (DateOnly?)s.SessionDate)
                                                 .FirstOrDefault()
                })
                .ToListAsync(cancellationToken);

            return Result.Success(new PagedResult<AllPatientsListItemDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            });
        }
    }
}
