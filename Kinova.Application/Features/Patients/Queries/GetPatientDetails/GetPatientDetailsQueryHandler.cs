
using Kinova.Application.Common.DTOs.PatientDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Patients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Patients.Queries.GetPatientDetails
{
    public sealed class GetPatientDetailsQueryHandler : IRequestHandler<GetPatientDetailsQuery, Result<PatientDetailsDto>>
    {
        private readonly IKinovaDbContext _context;

        public GetPatientDetailsQueryHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PatientDetailsDto>> Handle(GetPatientDetailsQuery request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients
                .Include(p => p.Doctor)
                .Where(p => p.UserId == request.UserId)
                .Select(p => new PatientDetailsDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CarePath = p.CarePath,
                    DoctorId = p.DoctorId,
                    DoctorName = p.Doctor.Name,
                    Gender = p.Gender,
                    Height = p.Height,
                    Weight = p.Weight,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (patient is null)
                return Result.Failure<PatientDetailsDto>(PatientErrors.UnAuthorized());

            return Result.Success(patient);
        }
    }
}
