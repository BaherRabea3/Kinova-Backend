
using Kinova.Domain.Entities.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinova.Infrastructure.Persistence.Configurations
{
    public sealed class ReportConfiguration
     : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.OverallScore)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(x => x.AverageRangeOfMotion)
                .HasPrecision(10, 3)
                .IsRequired();

            builder.Property(x => x.SummaryText)
                .HasMaxLength(5000)
                .IsRequired();

            builder.Property(x => x.TotalRepetitions)
                .IsRequired();

            builder.Property(x => x.CorrectRepetitions)
                .IsRequired();

            builder.HasOne(x => x.Session)
                .WithOne(x => x.Report)
                .HasForeignKey<Report>(x => x.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Doctor)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.SessionId)
                .IsUnique();

        }
    }
}
