using Kinova.Domain.Entities.JointAngles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinova.Infrastructure.Persistence.Configurations
{
    public sealed class JointAngleReadingConfiguration
    : IEntityTypeConfiguration<JointAngleReading>
    {
        public void Configure(EntityTypeBuilder<JointAngleReading> builder)
        {
            builder.ToTable("JointAngleReadings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.JointName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Angle)
                .HasPrecision(10, 3)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .IsRequired();

            builder.HasOne(j => j.Rep)
             .WithMany(r => r.AngleReadings)
             .HasForeignKey(j => j.RepId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(j => new
            {
                j.RepId,
                j.JointName
            });
        }
    }
}
