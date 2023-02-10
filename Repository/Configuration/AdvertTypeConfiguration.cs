using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class AdvertTypeConfiguration : IEntityTypeConfiguration<AdvertType>
{
    public void Configure(EntityTypeBuilder<AdvertType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        builder.Property(x => x.NameEn).IsRequired().HasMaxLength(20);
        builder.HasData(
           new AdvertType
           {
               Id = 1,
               Name = "Kuca",
               NameEn = "House"
           },
           new AdvertType
           {
               Id = 2,
               Name = "Stan",
               NameEn = "Apartman"
           },
           new AdvertType
           {
               Id = 3,
               Name = "Poslovni prostor",
               NameEn = "Office space"

           },
            new AdvertType
            {
                Id = 4,
                Name = "Vila",
                NameEn = "Villa"
            });
    }
}
