using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.Neighborhoods;
using KeyNekretnine.Domain.Shared;
using KeyNekretnine.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
internal sealed class AdvertConfiguration : IEntityTypeConfiguration<Advert>
{
    public void Configure(EntityTypeBuilder<Advert> builder)
    {
        builder.ToTable("adverts");

        builder.HasKey(advert => advert.Id);

        builder.Property(advert => advert.Price).IsRequired();

        builder.OwnsOne(advert => advert.Description, description =>
        {
            description.Property(d => d.Sr).HasMaxLength(1000).IsRequired();
            description.Property(d => d.En).HasMaxLength(1000).IsRequired(false);
        });

        builder.Property(advert => advert.NoOfBedrooms).IsRequired().HasMaxLength(100);

        builder.Property(advert => advert.NoOfBathrooms).IsRequired().HasMaxLength(100);

        builder.Property(advert => advert.FloorSpace).IsRequired().HasMaxLength(10000);

        builder.Property(advert => advert.HasElevator).IsRequired();

        builder.Property(advert => advert.HasGarage).IsRequired();

        builder.Property(advert => advert.HasTerrace).IsRequired();

        builder.Property(advert => advert.HasWifi).IsRequired();

        builder.Property(advert => advert.IsUrgent).IsRequired();

        builder.Property(advert => advert.IsUnderConstruction).IsRequired();

        builder.Property(advert => advert.IsFurnished).IsRequired();

        builder.Property(advert => advert.YearOfBuildingCreated).HasMaxLength(3000);

        builder.Property(advert => advert.CoverImageUrl)
            .HasMaxLength(150)
            .HasConversion(imageUrl => imageUrl.Value, value => new ImageUrl(value))
            .IsRequired();

        builder.OwnsOne(advert => advert.Location);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(advert => advert.UserId);

        builder.HasOne<Agent>()
            .WithMany()
            .HasForeignKey(advert => advert.AgentId);

        builder.HasOne<Neighborhood>()
            .WithMany()
            .HasForeignKey(advert => advert.NeighborhoodId)
            .IsRequired();
        //builder.HasMany(x => x.TemporeryImageDatas).WithOne(t => t.Advert).HasForeignKey(x => x.AdvertId);
        builder.Property(advert => advert.ReferenceId).HasMaxLength(10);
        builder.HasIndex(advert => advert.ReferenceId).IsUnique();

        builder.Property(advert => advert.CreatedOnDate).IsRequired();
    }
}