using Kinova.Domain.Entities.Patients;
using Kinova.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinova.Infrastructure.Persistence.Configurations
{
    public sealed class PatientConfiguration
    : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Patient>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Doctor)
                .WithMany(x => x.Patients)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasIndex(x => x.DoctorId);

        }
    }
}
