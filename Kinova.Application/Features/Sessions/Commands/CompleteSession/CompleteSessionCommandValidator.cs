
using FluentValidation;
using Kinova.Application.Common.DTOs.SessionDTOs;

namespace Kinova.Application.Features.Sessions.Commands.CompleteSession
{
    public class CompleteSessionCommandValidator : AbstractValidator<CompleteSessionCommand>
    {
        public CompleteSessionCommandValidator()
        {
            RuleFor(x => x.SessionId)
            .NotEmpty().WithMessage("SessionId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("PatientUserId is required.");

            RuleFor(x => x.Payload)
                .NotNull().WithMessage("Session completion payload is required.")
                .SetValidator(new CompleteSessionRequestValidator());
        }
    }
}
