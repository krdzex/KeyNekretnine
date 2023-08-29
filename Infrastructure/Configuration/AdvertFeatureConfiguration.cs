using KeyNekretnine.Domain.AdvertFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class AdvertFeatureConfiguration : IEntityTypeConfiguration<AdvertFeature>
{
    public void Configure(EntityTypeBuilder<AdvertFeature> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
    }
}
