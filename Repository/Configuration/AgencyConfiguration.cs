using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class AgencyConfiguration : IEntityTypeConfiguration<Agency>
{
    public void Configure(EntityTypeBuilder<Agency> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.HasOne(a => a.User).WithOne(i => i.Agency);
        builder.HasMany(a => a.ImaginaryAgents).WithOne(i => i.Agency).HasForeignKey(x => x.AgencyId);
    }
}
