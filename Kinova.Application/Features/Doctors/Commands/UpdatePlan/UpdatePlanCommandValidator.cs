using FluentValidation;

namespace Kinova.Application.Features.Doctors.Commands.UpdatePlan
{
    public class UpdatePlanCommandValidator : AbstractValidator<UpdatePlanCommand>
    {
        public UpdatePlanCommandValidator()
        {
            RuleFor(x => x.PlanId).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(2000);
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("end date must be after start date");
        }
    }
}
