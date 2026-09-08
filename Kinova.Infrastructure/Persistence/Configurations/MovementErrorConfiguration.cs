

using Kinova.Domain.Entities.MovementErrors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinova.Infrastructure.Persistence.Configurations
{
    public sealed class MovementErrorConfiguration
    : IEntityTypeConfiguration<MovementError>
    {
        public void Configure(EntityTypeBuilder<MovementError> builder)
        {
            builder.ToTable("MovementErrors");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.ErrorType)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Severity)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.HasOne(m => m.Rep)
                .WithMany(m => m.MovementErrors)
                .HasForeignKey(x => x.RepId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
