using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Shared;
using KeyNekretnine.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class AgencyConfiguration : IEntityTypeConfiguration<Agency>
{
    public void Configure(EntityTypeBuilder<Agency> builder)
    {
        builder.ToTable("agencies");

        builder.HasKey(agency => agency.Id);

        builder.Property(agency => agency.Name)
            .HasMaxLength(50)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();

        builder.Property(agency => agency.Description)
            .HasMaxLength(1000)
            .HasConversion(description => description.Value, value => new Description(value))
            .IsRequired(false);

        builder.Property(agency => agency.WebsiteUrl)
            .HasMaxLength(200)
            .HasConversion(websiteUrl => websiteUrl.Value, value => new WebsiteUrl(value))
            .IsRequired(false);

        builder.Property(agency => agency.ImageUrl)
            .HasMaxLength(200)
            .HasConversion(imageUrl => imageUrl.Value, value => new ImageUrl(value))
            .IsRequired(false);

        builder.Property(agency => agency.Email)
            .HasMaxLength(100)
            .HasConversion(email => email.Value, value => new Email(value))
            .IsRequired(false);

        builder.Property(agent => agent.PhoneNumber)
            .HasMaxLength(50)
            .HasConversion(phoneNumber => phoneNumber.Value, value => new PhoneNumber(value))
            .IsRequired(false);

        builder.OwnsOne(agency => agency.WorkHour);
        builder.OwnsOne(agency => agency.SocialMedia);
        builder.OwnsOne(agency => agency.Location);

        builder.Property(x => x.CreatedDate).IsRequired();

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Agency>(a => a.UserId);
    }
}