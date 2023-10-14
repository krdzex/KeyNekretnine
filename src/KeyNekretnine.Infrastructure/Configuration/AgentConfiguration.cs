using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.Shared;
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
            .HasConversion(firstName => firstName.Value, value => new FirstName(value))
            .IsRequired();

        builder.Property(agent => agent.LastName)
            .HasMaxLength(30)
            .HasConversion(lastName => lastName.Value, value => new LastName(value))
            .IsRequired();

        builder.Property(agent => agent.Description)
            .HasMaxLength(1000)
            .HasConversion(description => description.Value, value => new Description(value))
            .IsRequired(false);

        builder.Property(agent => agent.PhoneNumber)
            .HasMaxLength(50)
            .HasConversion(phoneNumber => phoneNumber.Value, value => new PhoneNumber(value))
            .IsRequired();

        builder.Property(agent => agent.ImageUrl)
            .HasMaxLength(200)
            .HasConversion(imageUrl => imageUrl.Value, value => new ImageUrl(value))
            .IsRequired(false);

        builder.Property(agent => agent.Email)
            .HasMaxLength(100)
            .HasConversion(email => email.Value, value => new Email(value))
            .IsRequired();

        builder.HasOne(agent => agent.Agency)
            .WithMany()
            .HasForeignKey(agent => agent.AgencyId);

        builder.OwnsOne(agent => agent.SocialMedia);
    }
}
