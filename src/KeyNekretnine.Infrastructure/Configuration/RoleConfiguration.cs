using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "548e52a6-485a-49a5-b204-8994eaa79a12",
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Id = "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            }
       );
    }
}