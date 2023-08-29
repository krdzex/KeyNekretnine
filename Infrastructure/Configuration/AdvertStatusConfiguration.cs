using KeyNekretnine.Domain.AdvertStatuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class AdvertStatusConfiguration : IEntityTypeConfiguration<AdvertStatus>
{
    public void Configure(EntityTypeBuilder<AdvertStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NameSr).IsRequired().HasMaxLength(20);
        builder.Property(x => x.NameEn).IsRequired().HasMaxLength(10);
        builder.HasData(
            new AdvertStatus
            {
                Id = 1,
                NameSr = "Prihvacen",
                NameEn = "Accepted"
            },
            new AdvertStatus
            {
                Id = 2,
                NameSr = "Na cekanju",
                NameEn = "Pending"
            },
            new AdvertStatus
            {
                Id = 3,
                NameSr = "Odbijen",
                NameEn = "Declined"
            },
            new AdvertStatus
            {
                Id = 4,
                NameSr = "Dodavanje u procesu",
                NameEn = "Uploading"
            });
    }
}