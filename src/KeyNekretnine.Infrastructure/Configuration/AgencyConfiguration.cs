using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Domain.ValueObjects;
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
            .HasConversion(name => name.Value, value => AgencyName.Create(value))
            .IsRequired();

        builder.Property(agency => agency.Description)
            .HasMaxLength(1000)
            .HasConversion(description => description.Value, value => Description.Create(value))
            .IsRequired(false);

        builder.Property(agency => agency.WebsiteUrl)
            .HasMaxLength(200)
            .HasConversion(websiteUrl => websiteUrl.Value, value => WebsiteUrl.Create(value))
            .IsRequired(false);

        builder.Property(agency => agency.ImageUrl)
            .HasMaxLength(200)
            .HasConversion(imageUrl => imageUrl.Value, value => ImageUrl.Create(value).Value)
            .IsRequired(false);

        builder.Property(agency => agency.Email)
            .HasMaxLength(100)
            .HasConversion(email => email.Value, value => Email.Create(value))
            .IsRequired(false);

        builder.Property(agent => agent.PhoneNumber)
            .HasMaxLength(50)
            .HasConversion(phoneNumber => phoneNumber.Value, value => PhoneNumber.Create(value))
            .IsRequired(false);

        builder.OwnsOne(agency => agency.WorkHour, timeRange =>
        {
            timeRange.Property(tr => tr.Start).IsRequired(false);

            timeRange.Property(tr => tr.End).IsRequired(false);
        });

        builder.OwnsOne(agency => agency.SocialMedia, socialMedia =>
        {
            socialMedia.Property(socialMedia => socialMedia.Twitter)
                .HasMaxLength(300)
                .IsRequired(false);

            socialMedia.Property(socialMedia => socialMedia.Facebook)
                .HasMaxLength(300)
                .IsRequired(false);

            socialMedia.Property(socialMedia => socialMedia.Instagram)
                .HasMaxLength(300)
                .IsRequired(false);

            socialMedia.Property(socialMedia => socialMedia.Linkedin)
                .HasMaxLength(300)
                .IsRequired(false);
        });

        builder.OwnsOne(agency => agency.Location, location =>
        {
            location.Property(location => location.Address)
                .HasMaxLength(300)
                .IsRequired(false);

            location.Property(location => location.Longitude)
                .IsRequired(false);

            location.Property(location => location.Latitude)
                .IsRequired(false);
        });

        builder.Property(x => x.CreatedDate).IsRequired();

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Agency>(a => a.UserId);
    }
}