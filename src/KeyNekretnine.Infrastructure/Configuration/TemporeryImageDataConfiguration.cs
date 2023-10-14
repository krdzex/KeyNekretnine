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
    }
}