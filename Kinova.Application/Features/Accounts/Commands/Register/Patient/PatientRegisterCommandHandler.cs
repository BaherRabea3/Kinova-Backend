using Kinova.Application.Common.DTOs.AccountDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Patients;
using MediatR;

namespace Kinova.Application.Features.Accounts.Commands.Register.Patient
{
    public sealed class PatientRegisterCommandHandler : IRequestHandler<PatientRegisterCommand, Result<AuthResponseDto>>
    {
        private readonly IKinovaDbContext _context;
        private readonly IAuthService _authService;
        public PatientRegisterCommandHandler(IKinovaDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<Result<AuthResponseDto>> Handle(PatientRegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await _authService.RegisterAsync(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                "Patient"
                );

            if (!response.IsAuthenticated)
            {
                return Result.Failure<AuthResponseDto>(Error.Validation("Account.ValidationError", response.Message));
            }

            var patient = new Kinova.Domain.Entities.Patients.Patient
            {
                UserId = response.UserId,
                Height = request.Height ?? 0,
                Weight = request.Weight ?? 0,
                Gender = request.Gender ?? Sex.Undefined
            };

            _context.Patients.Add(patient);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
