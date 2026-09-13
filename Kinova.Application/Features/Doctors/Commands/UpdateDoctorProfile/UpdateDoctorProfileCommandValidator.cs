using FluentValidation;

namespace Kinova.Application.Features.Doctors.Commands.UpdateDoctorProfile
{
    public class UpdateDoctorProfileCommandValidator : AbstractValidator<UpdateDoctorProfileCommand>
    {
        public UpdateDoctorProfileCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.LicenseNumber)
                .MaximumLength(100);

            RuleFor(x => x.Specialization)
                .MaximumLength(100);
        }
    }
}
