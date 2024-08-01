using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.ToTable("agents");

        builder.HasKey(x => x.Id);

        builder.Property(agent => agent.FirstName)
            .HasMaxLength(30)
            .HasConversion(firstName => firstName.Value, value => AgentFirstName.Create(value))
            .IsRequired();

        builder.Property(agent => agent.LastName)
            .HasMaxLength(30)
            .HasConversion(lastName => lastName.Value, value => AgentLastName.Create(value))
            .IsRequired();

        builder.Property(agent => agent.Description)
            .HasMaxLength(1000)
            .HasConversion(description => description.Value, value => Description.Create(value))
            .IsRequired(false);

        builder.Property(agent => agent.PhoneNumber)
            .HasMaxLength(50)
            .HasConversion(phoneNumber => phoneNumber.Value, value => AgentPhoneNumber.Create(value))
            .IsRequired();

        builder.Property(agent => agent.ImageUrl)
            .HasMaxLength(200)
            .HasConversion(imageUrl => imageUrl.Value, value => ImageUrl.Create(value).Value)
            .IsRequired(false);

        builder.Property(agent => agent.Email)
            .HasMaxLength(100)
            .HasConversion(email => email.Value, value => AgentEmail.Create(value))
            .IsRequired();


        builder.OwnsOne(agent => agent.SocialMedia, socialMedia =>
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

        builder.HasOne<Agency>(agent => agent.Agency)
            .WithMany(agency => agency.Agents)
            .HasForeignKey(agent => agent.AgencyId);
    }
}
