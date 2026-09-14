using Kinova.Domain.Entities.Scores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinova.Infrastructure.Persistence.Configurations
{
    public sealed class ScoreConfiguration
    : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.ToTable("Scores");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.OverallScore)
                .HasPrecision(5, 2)
                .IsRequired();


            builder.Property(x => x.RangeOfMotionScore)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(x => x.ValidRepetitions)
                .IsRequired();

            builder.Property(x => x.InvalidRepetitions)
                .IsRequired();

            builder.HasIndex(x => x.SessionId)
                .IsUnique();

            builder.HasOne(x => x.Session)
                .WithOne(x => x.Score)
                .HasForeignKey<Score>(x => x.SessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);

            builder.HasIndex(s => s.SessionId).IsUnique();   // enforces 1:1, backs the idempotent-retry lookup
        }
    }
}
