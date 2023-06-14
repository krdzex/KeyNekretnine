using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class ImaginaryAgentConfiguration : IEntityTypeConfiguration<ImaginaryAgent>
{
    public void Configure(EntityTypeBuilder<ImaginaryAgent> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ImageUrl).HasMaxLength(200);
    }
}
