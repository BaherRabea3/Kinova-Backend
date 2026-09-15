using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Doctors.Queries.GetDoctorProfile
{
    public sealed class GetDoctorProfileQueryHandler : IRequestHandler<GetDoctorProfileQuery, Result<DoctorProfileDto>>
    {
        private readonly IKinovaDbContext _context;

        public GetDoctorProfileQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<DoctorProfileDto>> Handle(GetDoctorProfileQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.UserId == request.UserId)
                .Select(d => new DoctorProfileDto
                {
                    Id = d.Id,
                    UserId = d.UserId,
                    Name = d.Name,
                    LicenseNumber = d.LicenseNumber,
                    Specialization = d.Specialization,
                    PatientsCount = d.Patients.Count
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (doctor is null)
                return Result.Failure<DoctorProfileDto>(DoctorErrors.UnAuthorized());

            doctor.Email = request.Email;

            return Result.Success(doctor);
        }
    }
}
