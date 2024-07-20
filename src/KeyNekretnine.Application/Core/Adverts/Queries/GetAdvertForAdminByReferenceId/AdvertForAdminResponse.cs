using KeyNekretnine.Application.Core.Shared;
using System.Text.Json.Serialization;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertForAdminByReferenceId;
public class AdvertForAdminResponse
{
    public int Price { get; set; }
    public string ReferenceId { get; set; }
    public double FloorSpace { get; set; }
    public int NoOfBedrooms { get; set; }
    public int NoOfBathrooms { get; set; }
    public int BuildingFloor { get; set; }
    public bool HasElevator { get; set; }
    public bool HasGarage { get; set; }
    public bool HasTerrace { get; set; }
    public bool HasWifi { get; set; }
    public bool IsFurnished { get; set; }
    public int YearOfBuildingCreated { get; set; }
    public bool IsUrgent { get; set; }
    public bool IsPremium { get; set; }
    public bool PendingUpdates { get; set; }
    public bool IsUnderConstruction { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
    public int Purpose { get; set; }

    [JsonIgnore]
    public string CoverImageUrl { get; set; }

    public DateTime CreatedOnDate { get; set; }
    public AdvertLocationResponse Location { get; set; }
    public AdvertDescriptionResponse Description { get; set; }
    public AdvertCreatorResponse Creator { get; set; }
    public List<AdvertImageResponse> Images { get; set; } = new();
    public List<AdvertFeatureResponse> Features { get; set; } = new();
}
