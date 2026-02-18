using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckInCloud.Api.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>

{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "8da8e3f1-8eec-41a5-a934-51e0c70ebeca",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = null
            },
            new IdentityRole
            {
                Id = "71da116a-0e6e-4bfb-b9f7-6e4a85de7076",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = null
            }
        );
    }
}