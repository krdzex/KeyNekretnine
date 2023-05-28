using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.AccountCreatedDate).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired(false);
        builder.Property(x => x.IsBanned).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.BanEnd).IsRequired(false);
        builder.Property(x => x.ProfileImageUrl).HasMaxLength(150);
        builder.Property(x => x.About).HasMaxLength(1000);

    }
}