using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Application.Features.Patients.Queries.GetAllDoctors;
using Kinova.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Patients.Queries.GetDoctors
{
    public sealed class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, Result<List<DoctorSummaryDto>>>
    {
        private readonly IKinovaDbContext _context;

        public GetDoctorsQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<DoctorSummaryDto>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Doctors.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(d => d.Name.Contains(request.Search));

            if (!string.IsNullOrWhiteSpace(request.Specialization))
                query = query.Where(d => d.Specialization == request.Specialization);

            var doctors = await query
                .OrderBy(d => d.Name)
                .Select(d => new DoctorSummaryDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Specialization = d.Specialization,
                    LicenseNumber = d.LicenseNumber,
                    PatientsCount = d.Patients.Count
                })
                .ToListAsync(cancellationToken);

            return Result.Success(doctors);
        }
    }
}