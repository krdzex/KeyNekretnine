using KeyNekretnine.Domain.AdvertPurposes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class AdvertPurposeConfiguration : IEntityTypeConfiguration<AdvertPurpose>
{
    public void Configure(EntityTypeBuilder<AdvertPurpose> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NameSr).IsRequired().HasMaxLength(15);
        builder.HasData(
           new AdvertPurpose
           {
               Id = 1,
               NameSr = "Rentiranje",
               NameEn = "Rent"
           },
           new AdvertPurpose
           {
               Id = 2,
               NameSr = "Prodaja",
               NameEn = "Sale"
           },
           new AdvertPurpose
           {
               Id = 3,
               NameSr = "Stan na dan",
               NameEn = "Short term rent"
           });
    }
}