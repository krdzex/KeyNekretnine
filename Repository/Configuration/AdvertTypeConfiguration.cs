using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class AdvertTypeConfiguration : IEntityTypeConfiguration<AdvertType>
{
    public void Configure(EntityTypeBuilder<AdvertType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NameSr).IsRequired().HasMaxLength(20);
        builder.Property(x => x.NameEn).IsRequired().HasMaxLength(20);
        builder.HasData(
           new AdvertType
           {
               Id = 1,
               NameSr = "Kuca",
               NameEn = "House"
           },
           new AdvertType
           {
               Id = 2,
               NameSr = "Stan",
               NameEn = "Apartman"
           },
           new AdvertType
           {
               Id = 3,
               NameSr = "Poslovni prostor",
               NameEn = "Office space"

           },
            new AdvertType
            {
                Id = 4,
                NameSr = "Vila",
                NameEn = "Villa"
            });
    }
}
