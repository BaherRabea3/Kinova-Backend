using Kinova.Application.Common.DTOs.AccountDTOs;
using Kinova.Application.Common.Interfaces;
using Kinova.Domain.Common;
using Kinova.Domain.Entities.Doctors;
using MediatR;

namespace Kinova.Application.Features.Accounts.Commands.Register.Doctor
{
    public sealed class DoctorRegisterCommandHandler : IRequestHandler<DoctorRegisterCommand, Result<AuthResponseDto>>
    {
        private readonly IKinovaDbContext _context;
        private readonly IAuthService _authService;
        public DoctorRegisterCommandHandler(IKinovaDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<Result<AuthResponseDto>> Handle(DoctorRegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await _authService.RegisterAsync(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                "Doctor"
                );

            if (!response.IsAuthenticated)
            {
                return Result.Failure<AuthResponseDto>(Error.Validation("Account.ValidationError", response.Message));
            }

            Kinova.Domain.Entities.Doctors.Doctor doctor = new Kinova.Domain.Entities.Doctors.Doctor()
            {
                UserId = response.UserId,
                LicenseNumber = request.LicenseNumber ?? null,
                Specialization = request.Specialization ?? null
            };
            _context.Doctors.Add(doctor);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
