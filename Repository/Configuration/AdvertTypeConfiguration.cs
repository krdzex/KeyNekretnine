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
        builder.HasData(
           new AdvertType
           {
               Id = 1,
               Name = "Kuca"
           },
           new AdvertType
           {
               Id = 2,
               Name = "Stan"
           },
           new AdvertType
           {
               Id = 3,
               Name = "Poslovni prostor"
           },
            new AdvertType
            {
                Id = 4,
                Name = "Vila"
            });
    }
}
