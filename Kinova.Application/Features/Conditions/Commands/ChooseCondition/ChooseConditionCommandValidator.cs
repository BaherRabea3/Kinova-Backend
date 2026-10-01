using FluentValidation;

namespace Kinova.Application.Features.Conditions.Commands.ChooseCondition
{
    public class ChooseConditionCommandValidator : AbstractValidator<ChooseConditionCommand>
    {
        public ChooseConditionCommandValidator()
        {
            RuleFor(x => x.ConditionId).NotEmpty();
        }
    }
}