﻿namespace KeyNekretnine.Api.Controllers.Adverts;

public record AdvertMapParameters
{
    public int MinPrice { get; init; } = 0;
    public int MaxPrice { get; init; } = int.MaxValue;
    public int MinFloorSpace { get; init; } = 0;
    public int MaxFloorSpace { get; init; } = int.MaxValue;
    public List<int> NoOfBedrooms { get; init; }
    public List<int> NoOfBathrooms { get; init; }
    public int? Type { get; init; }
    public int? Purpose { get; init; }
    public string CitySlug { get; init; }
    public List<int> Neighborhoods { get; init; }
    public bool? IsEmergency { get; init; }
    public bool? IsUnderConstruction { get; init; }
    public bool? IsFurnished { get; init; }
}