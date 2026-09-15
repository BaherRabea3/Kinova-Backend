using Kinova.Application.Common.DTOs.PatientDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.Patients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Patients.Commands.FollowDoctor
{
    public sealed class FollowDoctorCommandHandler : IRequestHandler<FollowDoctorCommand, Result<PatientDetailsDto>>
    {
        private readonly IKinovaDbContext _context;

        public FollowDoctorCommandHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PatientDetailsDto>> Handle(FollowDoctorCommand request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == request.UserId, cancellationToken);

            if (patient is null)
                return Result.Failure<PatientDetailsDto>(PatientErrors.UnAuthorized());

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == request.DoctorId, cancellationToken);

            if (doctor is null)
                return Result.Failure<PatientDetailsDto>(DoctorErrors.NotFound());

            patient.DoctorId = doctor.Id;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new PatientDetailsDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Gender = patient.Gender,
                Height = patient.Height,
                Weight = patient.Weight,
                CarePath = patient.CarePath,
                DoctorId = patient.DoctorId,
                DoctorName = doctor.Name
            });
        }
    }
}
