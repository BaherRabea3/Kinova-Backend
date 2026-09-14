using FluentValidation;

namespace Kinova.Application.Common.DTOs.RepDTOs
{
    public class RepUploadValidator : AbstractValidator<RepUploadDto>
    {
        public RepUploadValidator()
        {
            RuleFor(r => r.RepNumber).GreaterThan(0);
            RuleFor(r => r.EndTime).GreaterThanOrEqualTo(r => r.StartTime);
            RuleForEach(r => r.JointAngleReadings)
                .ChildRules(j => j.RuleFor(x => x.Angle).InclusiveBetween(-180, 180));
        }
    }
}
