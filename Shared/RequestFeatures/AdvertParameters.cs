﻿namespace Shared.RequestFeatures;

public class AdvertParameters : RequestParameters
{
    public AdvertParameters() => OrderBy = "created_date";
    public int MinPrice { get; set; } = 0;
    public int MaxPrice { get; set; } = int.MaxValue;
    public int MinFloorSpace { get; set; } = 0;
    public int MaxFloorSpace { get; set; } = int.MaxValue;
    public IEnumerable<Int32> NoOfBedrooms { get; set; }
    public IEnumerable<Int32> NoOfBathrooms { get; set; }
    public IEnumerable<Int32> AdvertTypeIds { get; set; }
    public IEnumerable<Int32> AdvertPurposeIds { get; set; }
    public int? CityId { get; set; }
    public IEnumerable<Int32> NeighborhoodIds { get; set; }
    public bool IsEmergency { get; set; }
    public bool IsUnderConstruction { get; set; }
    public bool IsFurnished { get; set; }

}