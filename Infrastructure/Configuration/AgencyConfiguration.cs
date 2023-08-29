using KeyNekretnine.Domain.Agencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class AgencyConfiguration : IEntityTypeConfiguration<Agency>
{
    public void Configure(EntityTypeBuilder<Agency> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.HasOne(a => a.User).WithOne(i => i.Agency);
        builder.HasMany(a => a.Agents).WithOne(i => i.Agency).HasForeignKey(x => x.AgencyId);
        builder.Property(x => x.Address).HasMaxLength(200);
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.WebsiteUrl).HasMaxLength(200);
        builder.Property(x => x.WorkStartTime).IsRequired(false);
        builder.Property(x => x.WorkEndTime).IsRequired(false);
        builder.Property(x => x.TwitterUrl).HasMaxLength(200);
        builder.Property(x => x.FacebookUrl).HasMaxLength(200);
        builder.Property(x => x.InstagramUrl).HasMaxLength(200);
        builder.Property(x => x.LinkedinUrl).HasMaxLength(200);
        builder.Property(x => x.Latitude).IsRequired(false);
        builder.Property(x => x.Longitude).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.Property(x => x.ImageUrl).HasMaxLength(200);
    }
}