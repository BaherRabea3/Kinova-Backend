using Kinova.Application.Common.DTOs.CommonDTOs;
using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Doctors.Queries.GetPatients
{
    public sealed class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, Result<PagedResult<DoctorPatientListItemDto>>>
    {
        private const int MaxPageSize = 50;

        private readonly IKinovaDbContext _context;

        public GetPatientsQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedResult<DoctorPatientListItemDto>>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.UserId == request.DoctorUserId, cancellationToken);

            if (doctor is null)
                return Result.Failure<PagedResult<DoctorPatientListItemDto>>(DoctorErrors.UnAuthorized());

            var page = request.Page < 1 ? 1 : request.Page;
            var pageSize = request.PageSize < 1 ? 10 : Math.Min(request.PageSize, MaxPageSize);

            var query = _context.Patients
                .AsNoTracking()
                .Where(p => p.DoctorId == doctor.Id);

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
                .Select(p => new DoctorPatientListItemDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Gender = p.Gender,
                    Height = p.Height,
                    Weight = p.Weight,
                    CarePath = p.CarePath,
                    Status = p.Plans.Any(pl => pl.IsActive) ? "Active" : "Inactive",
                    ActivePlanName = p.Plans.Where(pl => pl.IsActive)
                                             .OrderByDescending(pl => pl.StartDate)
                                             .Select(pl => pl.Name)
                                             .FirstOrDefault(),
                    TotalSessions = p.Sessions.Count,
                    LastSessionDate = p.Sessions.OrderByDescending(s => s.SessionDate)
                                                 .Select(s => (DateOnly?)s.SessionDate)
                                                 .FirstOrDefault()
                })
                .ToListAsync(cancellationToken);

            return Result.Success(new PagedResult<DoctorPatientListItemDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            });
        }
    }
}
