

using Kinova.Domain.Common;

namespace Kinova.Domain.Entities.PlanExerciseItems
{
    public static class PlanExerciseItemErrors
    {
        public static Error NotInPatientPlan()
            => Error.Forbidden("PlanExerciseItem.NotInPatientPlan", "Exercise item not in patient's active plan.");
    }
}
