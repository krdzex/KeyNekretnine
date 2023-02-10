using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class AdvertPurposeConfiguration : IEntityTypeConfiguration<AdvertPurpose>
{
    public void Configure(EntityTypeBuilder<AdvertPurpose> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(15);
        builder.HasData(
           new AdvertPurpose
           {
               Id = 1,
               Name = "Rentiranje",
               NameEn = "Rent"
           },
           new AdvertPurpose
           {
               Id = 2,
               Name = "Prodaja",
               NameEn = "Sale"
           },
           new AdvertPurpose
           {
               Id = 3,
               Name = "Stan na dan",
               NameEn = "Short term rent"
           });
    }
}
