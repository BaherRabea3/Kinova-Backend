using Kinova.Domain.Common;

namespace Kinova.Domain.Entities.Doctors
{
    public static class DoctorErrors
    {
        public static Error UnAuthorized()
            => Error.UnAuthorized("Doctor.UnAuthorized", "you must login first");

        public static Error PatientNotFound()
            => Error.NotFound("Doctor.PatientNotFound", "patient not found");

        public static Error NotYourPatient()
            => Error.Forbidden("Doctor.NotYourPatient", "this patient is not assigned to you");
    }
}
