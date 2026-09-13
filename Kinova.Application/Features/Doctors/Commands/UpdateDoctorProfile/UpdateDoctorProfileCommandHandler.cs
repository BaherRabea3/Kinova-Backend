using Kinova.Application.Common.DTOs.DoctorDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Doctors.Commands.UpdateDoctorProfile
{
    public sealed class UpdateDoctorProfileCommandHandler : IRequestHandler<UpdateDoctorProfileCommand, Result<DoctorProfileDto>>
    {
        private readonly IKinovaDbContext _context;

        public UpdateDoctorProfileCommandHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<DoctorProfileDto>> Handle(UpdateDoctorProfileCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Patients)
                .FirstOrDefaultAsync(d => d.UserId == request.UserId, cancellationToken);

            if (doctor is null)
                return Result.Failure<DoctorProfileDto>(DoctorErrors.UnAuthorized());

            doctor.Name = request.Name;
            doctor.LicenseNumber = request.LicenseNumber;
            doctor.Specialization = request.Specialization;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new DoctorProfileDto
            {
                Id = doctor.Id,
                UserId = doctor.UserId,
                Name = doctor.Name,
                Email = request.Email,
                LicenseNumber = doctor.LicenseNumber,
                Specialization = doctor.Specialization,
                PatientsCount = doctor.Patients.Count
            });
        }
    }
}
