﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Configuration.SeedData;

namespace Repository.Configuration;
public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.GeoId).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(200);
        builder.HasData(CitiesData.GetCities());
    }
}
