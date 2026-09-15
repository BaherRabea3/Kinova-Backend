using FluentValidation;


namespace Kinova.Application.Features.Patients.Commands.FollowDoctor
{
    public class FollowDoctorCommandValidator : AbstractValidator<FollowDoctorCommand>
    {
        public FollowDoctorCommandValidator()
        {
            RuleFor(x => x.DoctorId).NotEmpty();
        }
    }
}
