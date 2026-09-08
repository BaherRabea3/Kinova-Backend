using FluentValidation;
using Kinova.Application.Features.Accounts.Commands.Register.Doctor;

namespace Kinova.Application.Features.Accounts.Commands.Register.Patient
{
    public class DoctorRegisterCommandValidator : AbstractValidator<DoctorRegisterCommand>
    {
        public DoctorRegisterCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name can't be null");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name can't be null");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can't be null")
                .EmailAddress().WithMessage("Invalid Email Address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can't be null");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
