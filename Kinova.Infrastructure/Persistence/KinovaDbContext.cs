using Kinova.Application.Common.Interfaces;
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
using Kinova.Infrastructure.Identity;
using Kinova.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Kinova.Infrastructure.Persistence
{
    public class KinovaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IKinovaDbContext
    {
        public KinovaDbContext(
       DbContextOptions<KinovaDbContext> options)
       : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<PlanExerciseItem> PlanExercises { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Rep> Reps { get; set; }

        public DbSet<JointAngleReading> JointAngleReadings { get; set; }

        public DbSet<MovementError> MovementErrors { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfigurationAssemblyMarker).Assembly);
        }


    }
}
