using KeyNekretnine.Domain.Neighborhoods;
using KeyNekretnine.Infrastructure.Configuration.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyNekretnine.Infrastructure.Configuration;
public class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
{
    public void Configure(EntityTypeBuilder<Neighborhood> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.HasOne(x => x.City).WithMany(x => x.Neighborhoods).HasForeignKey(x => x.CityId).IsRequired();
        builder.HasData(NeighborhoodData.GetNeighborhoods());
    }
}