using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class TemporeryImageDataConfiguration : IEntityTypeConfiguration<TemporeryImageData>
    {
        public void Configure(EntityTypeBuilder<TemporeryImageData> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageData).IsRequired();
            builder.Property(x => x.IsCover).IsRequired().HasDefaultValue(false);
        }
    }

}
