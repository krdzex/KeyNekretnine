using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class AdvertStatusConfiguration : IEntityTypeConfiguration<AdvertStatus>
{
    public void Configure(EntityTypeBuilder<AdvertStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(10);
        builder.HasData(
            new AdvertStatus
            {
                Id = 1,
                Name = "Prihvacen"
            },
            new AdvertStatus
            {
                Id = 2,
                Name = "Na cekanju"
            },
            new AdvertStatus
            {
                Id = 3,
                Name = "Odbijen"
            },
            new AdvertStatus
            {
                Id = 4,
                Name = "Uploading"
            });
    }
}
