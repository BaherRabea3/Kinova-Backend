using Kinova.Domain.Common;

namespace Kinova.Domain.Entities.Patients
{
    public static class PatientErrors
    {
        public static Error NotYourSession()
            => Error.Forbidden("Patient.NotYourSession", "Not Your Session");
    }
}
