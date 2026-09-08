using Kinova.Domain.Entities.Doctors;
using Kinova.Domain.Entities.Exercises;
using Kinova.Domain.Entities.JointAngles;
using Kinova.Domain.Entities.MovementErrors;
using Kinova.Domain.Entities.Patients;
using Kinova.Domain.Entities.PlanExerciseItems;
using Kinova.Domain.Entities.Plans;
using Kinova.Domain.Entities.Reports;
using Kinova.Domain.Entities.Reps;
using Kinova.Domain.Entities.Scores;
using Kinova.Domain.Entities.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Kinova.Application.Common.Interfaces
{
    public interface IKinovaDbContext
    {
         DbSet<Doctor> Doctors { get; }

         DbSet<Patient> Patients { get; }

         DbSet<Exercise> Exercises { get; }

         DbSet<Plan> Plans { get; }

         DbSet<PlanExerciseItem> PlanExercises { get; }

         DbSet<Session> Sessions { get; }

         DbSet<Rep> Reps { get; }

         DbSet<JointAngleReading> JointAngleReadings { get; }

         DbSet<MovementError> MovementErrors { get; }

         DbSet<Score> Scores { get; }

         DbSet<Report> Reports { get; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);


    }
}
