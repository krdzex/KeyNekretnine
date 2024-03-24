using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.Neighborhoods;
using KeyNekretnine.Domain.Users;
using KeyNekretnine.Domain.ValueObjects;
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

        builder.Property(advert => advert.YearOfBuildingCreated).HasMaxLength(3000);

        builder.Property(advert => advert.CoverImageUrl)
            .HasMaxLength(150)
            .HasConversion(imageUrl => imageUrl.Value, value => ImageUrl.Create(value).Value)
            .IsRequired(false);

        builder.OwnsOne(advert => advert.Location, location =>
        {
            location.Property(location => location.Address)
                .HasMaxLength(300)
                .IsRequired(false);

            location.Property(location => location.Longitude)
                .IsRequired(false);

            location.Property(location => location.Latitude)
                .IsRequired(false);
        });

        builder.HasOne<User>(advert => advert.User)
            .WithMany(user => user.Adverts)
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