using Kinova.Domain.Common;

namespace Kinova.Domain.Entities.Conditions
{
    public static class ConditionErrors
    {
        public static Error NotFound()
            => Error.NotFound("Condition.NotFound", "condition not found");
    }
}
