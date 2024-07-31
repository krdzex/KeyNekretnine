using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("images");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Url).IsRequired().HasMaxLength(200);

        builder.HasOne<Advert>()
            .WithMany(advert => advert.AdvertImages)
            .HasForeignKey(image => image.AdvertId);
    }
}