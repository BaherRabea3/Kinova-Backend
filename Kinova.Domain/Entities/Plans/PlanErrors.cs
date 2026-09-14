using Kinova.Domain.Common;

namespace Kinova.Domain.Entities.Plans
{
    public static class PlanErrors
    {
        public static Error NotFound()
            => Error.NotFound("Plan.NotFound", "plan not found");

        public static Error NotYourPlan()
            => Error.Forbidden("Plan.NotYourPlan", "this plan does not belong to one of your patients");

        public static Error InvalidDateRange()
            => Error.Validation("Plan.InvalidDateRange", "end date must be after start date");

        public static Error ExerciseNotFound(Guid exerciseId)
            => Error.NotFound("Plan.ExerciseNotFound", $"exercise '{exerciseId}' not found");

        public static Error NoExercises()
            => Error.Validation("Plan.NoExercises", "a plan must contain at least one exercise");
    }
}
