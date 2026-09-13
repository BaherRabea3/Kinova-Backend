using FluentValidation;

namespace Kinova.Application.Features.Doctors.Commands.CreatePlan
{
    public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty();

            RuleFor(x => x.Name).MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(2000);

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("end date must be after start date");

            RuleFor(x => x.Exercises)
                .NotEmpty()
                .WithMessage("a plan must contain at least one exercise");

            RuleForEach(x => x.Exercises).ChildRules(e =>
            {
                e.RuleFor(i => i.ExerciseId).NotEmpty();
                e.RuleFor(i => i.Sets).GreaterThan(0);
                e.RuleFor(i => i.Repetitions).GreaterThan(0);
                e.RuleFor(i => i.FrequencyPerWeek).GreaterThan(0);
            });
        }
    }
}
