using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;
public class AdvertConfiguration : IEntityTypeConfiguration<Advert>
{
    public void Configure(EntityTypeBuilder<Advert> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.DescriptionSr).IsRequired().HasMaxLength(10000);
        builder.Property(x => x.DescriptionEn).HasMaxLength(10000);
        builder.HasOne(x => x.Purpose).WithMany(x => x.Adverts).HasForeignKey(x => x.PurposeId).IsRequired();
        builder.Property(x => x.PurposeId).HasDefaultValue(1);
        builder.HasOne(x => x.Status).WithMany(x => x.Adverts).HasForeignKey(x => x.StatusId).IsRequired();
        builder.Property(x => x.StatusId).HasDefaultValue(1);
        builder.HasOne(x => x.Type).WithMany(x => x.Adverts).HasForeignKey(x => x.TypeId).IsRequired();
        builder.Property(x => x.TypeId).HasDefaultValue(1);
        builder.Property(x => x.NoOfBedrooms).IsRequired().HasMaxLength(100);
        builder.Property(x => x.NoOfBathrooms).IsRequired().HasMaxLength(100);
        builder.Property(x => x.FloorSpace).IsRequired().HasMaxLength(10000);
        builder.Property(x => x.HasElevator).IsRequired();
        builder.Property(x => x.HasGarage).IsRequired();
        builder.Property(x => x.HasTerrace).IsRequired();
        builder.Property(x => x.HasWifi).IsRequired();
        builder.Property(x => x.IsFurnished).IsRequired();
        builder.Property(x => x.YearOfBuildingCreated).HasMaxLength(3000);
        builder.Property(x => x.CoverImageUrl).IsRequired().HasMaxLength(150);
        builder.Property(x => x.CoverImageBlurUrl).HasMaxLength(250);
        builder.Property(x => x.Street).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Latitude).IsRequired().HasMaxLength(91);
        builder.Property(x => x.Longitude).IsRequired().HasMaxLength(181);
        builder.HasOne(a => a.User).WithMany(u => u.Adverts).HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany(a => a.Images).WithOne(i => i.Advert).HasForeignKey(x => x.AdvertId);
        builder.HasOne(x => x.Neighborhood).WithMany(x => x.Adverts).HasForeignKey(x => x.NeighborhoodId).IsRequired();
        builder.Property(x => x.NeighborhoodId).HasDefaultValue(1);
        builder.HasMany(x => x.TemporeryImageDatas).WithOne(t => t.Advert).HasForeignKey(x => x.AdvertId);
        builder.Property(x => x.ReferenceId).HasMaxLength(10);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.HasMany(a => a.Features).WithOne(i => i.Advert).HasForeignKey(x => x.AdvertId);
    }
}