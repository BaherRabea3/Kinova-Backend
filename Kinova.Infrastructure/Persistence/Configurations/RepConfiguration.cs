using Kinova.Domain.Entities.Reps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinova.Infrastructure.Persistence.Configurations
{
    public sealed class RepConfiguration
    : IEntityTypeConfiguration<Rep>
    {
        public void Configure(EntityTypeBuilder<Rep> builder)
        {
            builder.ToTable("Reps");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");


            builder.Property(x => x.IsCorrect)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.SessionId,
                x.RepNumber
            })
            .IsUnique();
        }
    }
}
