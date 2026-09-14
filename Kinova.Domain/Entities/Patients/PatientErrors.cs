using Kinova.Domain.Common;

namespace Kinova.Domain.Entities.Patients
{
    public static class PatientErrors
    {
        public static Error NotYourSession()
            => Error.Forbidden("Patient.NotYourSession", "Not Your Session");
        public static Error UnAuthorized()
           => Error.UnAuthorized("Patient.UnAuthorized", "you must login first");
    }
}
