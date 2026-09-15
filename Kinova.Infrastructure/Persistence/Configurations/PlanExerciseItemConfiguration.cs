using Kinova.Domain.Entities.PlanExerciseItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinova.Infrastructure.Persistence.Configurations
{
    public sealed class PlanExerciseItemConfiguration
    : IEntityTypeConfiguration<PlanExerciseItem>
    {
        public void Configure(EntityTypeBuilder<PlanExerciseItem> builder)
        {
            builder.ToTable("PlanExercises");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Sets)
                .IsRequired();

            builder.Property(x => x.Repetitions)
                .IsRequired();

            builder.HasOne(x => x.Plan)
                .WithMany(x => x.Exercises)
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Exercise)
                .WithMany(x => x.PlanExercises)
                .HasForeignKey(x => x.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.PlanId,
                x.ExerciseId
            })
            .IsUnique();

            builder.HasData(DbSeeder.SeedPlanExerciseItems());

        }
    }
}
