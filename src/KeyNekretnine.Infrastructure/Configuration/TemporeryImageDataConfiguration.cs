using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.TemporeryImageDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class TemporeryImageDataConfiguration : IEntityTypeConfiguration<TemporeryImageData>
{
    public void Configure(EntityTypeBuilder<TemporeryImageData> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ImageData).IsRequired();
        builder.Property(x => x.IsCover).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.CreatedDate).IsRequired();

        builder.HasOne<Advert>()
            .WithMany()
            .HasForeignKey(x => x.AdvertId);

    }
}