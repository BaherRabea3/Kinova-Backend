using Kinova.Domain.Entities.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinova.Infrastructure.Persistence.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()"); 

            builder.Property(s => s.SessionDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(s => s.StartTime)
                .IsRequired();


            builder.Property(s => s.CorrectReps)
                .HasDefaultValue(0);

            // Relationships
            builder.HasOne(s => s.Patient)
                .WithMany(p => p.Sessions)
                .HasForeignKey(s => s.PatientId)
                .OnDelete(DeleteBehavior.Restrict);   

            builder.HasOne(s => s.Exercise)
                .WithMany()
                .HasForeignKey(s => s.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.PlanExerciseItem)
                .WithMany(i => i.Sessions)
                .HasForeignKey(s => s.ItemId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(s => s.Reps)
                .WithOne(r => r.Session)
                .HasForeignKey(r => r.SessionId)
                .OnDelete(DeleteBehavior.Cascade);    

            builder.HasIndex(s => new { s.PatientId, s.SessionDate })
                .HasDatabaseName("IX_Sessions_Patient_Date");  

            builder.HasIndex(s => s.ExerciseId);
        }
    }
}
