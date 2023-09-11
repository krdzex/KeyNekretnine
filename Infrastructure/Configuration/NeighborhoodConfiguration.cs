using KeyNekretnine.Domain.Cities;
using KeyNekretnine.Domain.Neighborhoods;
using KeyNekretnine.Infrastructure.Configuration.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
{
    public void Configure(EntityTypeBuilder<Neighborhood> builder)
    {
        builder.ToTable("neighborhoods");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasData(NeighborhoodData.GetNeighborhoods());

        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(neighborhood => neighborhood.CityId);
    }
}