using FluentValidation;
using Kinova.Application.Common.DTOs.RepDTOs;

namespace Kinova.Application.Common.DTOs.SessionDTOs
{
    public class CompleteSessionRequestValidator : AbstractValidator<CompleteSessionRequest>
    {
        public CompleteSessionRequestValidator()
        {
            RuleFor(x => x.Reps).NotEmpty().WithMessage("At least one rep is required to complete a session.");

            RuleFor(x => x.Reps)
                .Must(reps => reps.Select(r => r.RepNumber).Distinct().Count() == reps.Count)
                .WithMessage("Duplicate RepNumber values in payload.");

            RuleForEach(x => x.Reps).SetValidator(new RepUploadValidator());
        }
    }
}
