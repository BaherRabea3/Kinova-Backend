using Kinova.Domain.Common;

namespace Kinova.Domain.Entities.Sessions
{
    public static class SessionErrors
    {
        public static Error NotFound()
            => Error.NotFound("Session.NotFound", "Session not found");
        public static Error CannotCompleted()
            => Error.Conflict("Session.CannotCompleted", "Session was cancelled and cannot be completed.");
        public static Error CannotCanceled()
            => Error.Conflict("Session.CannotCanceled", "Cannot cancel a completed session.");
    }
}
