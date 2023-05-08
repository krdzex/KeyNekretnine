using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects.Advert;
public class AddAdvertDto
{
    public double Price { get; set; }

    public string DescriptionSr { get; set; }
    public string DescriptionEn { get; set; }

    public int NoOfBedrooms { get; set; }
    public double FloorSpace { get; set; }
    public int NoOfBathrooms { get; set; }
    public bool? HasTerrace { get; set; }
    public bool? HasGarage { get; set; }
    public bool? IsFurnished { get; set; }
    public bool? HasWifi { get; set; }
    public bool? HasElevator { get; set; }
    public int BuildingFloor { get; set; }
    public int? YearOfBuildingCreated { get; set; }
    public int AdvertTypeId { get; set; }
    public int AdvertPurposeId { get; set; }
    public IFormFileCollection ImageFiles { get; set; }
    public IFormFile CoverImage { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Street { get; set; }
    public int NeighborhoodId { get; set; }
    public IEnumerable<string> Features { get; set; }
    public bool IsEmergency { get; set; }
    public bool IsUnderConstruction { get; set; }
}
