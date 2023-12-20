using KeyNekretnine.Domain.Users;
using KeyNekretnine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.FirstName)
            .HasMaxLength(50)
            .HasConversion(firstName => firstName.Value, value => UserFirstName.Create(value))
            .IsRequired(false);

        builder.Property(user => user.LastName)
            .HasMaxLength(50)
            .HasConversion(lastName => lastName.Value, value => UserLastName.Create(value))
            .IsRequired(false);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.AccountCreatedDate).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired(false);
        builder.Property(x => x.IsBanned)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.BanEnd).IsRequired(false);

        builder.Property(user => user.ProfileImageUrl)
            .HasMaxLength(150)
            .HasConversion(profileImageUrl => profileImageUrl.Value, value => new ProfileImageUrl(value))
            .IsRequired(false);

        builder.Property(user => user.About)
            .HasMaxLength(150)
            .HasConversion(about => about.Value, value => new About(value))
            .IsRequired(false);
    }
}