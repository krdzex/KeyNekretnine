using KeyNekretnine.Domain.AdvertFeatures;
using KeyNekretnine.Domain.Adverts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class AdvertFeatureConfiguration : IEntityTypeConfiguration<AdvertFeature>
{
    public void Configure(EntityTypeBuilder<AdvertFeature> builder)
    {
        builder.ToTable("advert_features");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);


        builder.HasOne<Advert>()
            .WithMany(advert => advert.AdvertFeatures)
            .HasForeignKey(feature => feature.AdvertId);
    }
}
