using Kinova.Application.Common.DTOs.PatientDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Conditions;
using Kinova.Domain.Entities.Patients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kinova.Application.Features.Conditions.Commands.ChooseCondition
{
    public sealed class ChooseConditionCommandHandler : IRequestHandler<ChooseConditionCommand, Result<PatientDetailsDto>>
    {
        private readonly IKinovaDbContext _context;

        public ChooseConditionCommandHandler(IKinovaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PatientDetailsDto>> Handle(ChooseConditionCommand request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients
                .Include(p => p.Doctor)
                .FirstOrDefaultAsync(p => p.UserId == request.UserId, cancellationToken);

            if (patient is null)
                return Result.Failure<PatientDetailsDto>(PatientErrors.UnAuthorized());

            var condition = await _context.Conditions
                .FirstOrDefaultAsync(c => c.Id == request.ConditionId, cancellationToken);

            if (condition is null)
                return Result.Failure<PatientDetailsDto>(ConditionErrors.NotFound());

            patient.ConditionId = condition.Id;
            patient.DiagnosedDate = DateOnly.FromDateTime(DateTime.UtcNow);

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
                DoctorName = patient.Doctor?.Name ?? string.Empty,
                ConditionId = patient.ConditionId,
                ConditionName = condition.Name,
                DiagnosedDate = patient.DiagnosedDate
            });
        }
    }
}