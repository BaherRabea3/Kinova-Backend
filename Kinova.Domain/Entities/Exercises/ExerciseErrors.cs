
using Kinova.Domain.Common;

namespace Kinova.Domain.Entities.Exercises
{
    public static class ExerciseErrors
    {
        public static Error NotFound =>
            Error.NotFound("Exercise.NotFound", "Exercise not found");
    }
}
