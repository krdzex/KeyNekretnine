using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.HasKey(x => x.Id);
        //builder.HasIndex(x => new { x.Id, x.Email }).IsUnique();
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ImageUrl).HasMaxLength(200);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.TwitterUrl).HasMaxLength(200);
        builder.Property(x => x.FacebookUrl).HasMaxLength(200);
        builder.Property(x => x.InstagramUrl).HasMaxLength(200);
        builder.Property(x => x.LinkedinUrl).HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(1000);
    }
}
