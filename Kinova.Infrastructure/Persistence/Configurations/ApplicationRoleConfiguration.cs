
using Kinova.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kinova.Infrastructure.Persistence.Configurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
                new ApplicationRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "Patient",
                    NormalizedName = "PATIENT"
                },
                new ApplicationRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "Doctor",
                    NormalizedName = "DOCTOR"
                });


        }
    }
}
