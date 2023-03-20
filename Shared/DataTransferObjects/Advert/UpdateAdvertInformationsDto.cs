namespace Shared.DataTransferObjects.Advert;
public class UpdateAdvertInformationsDto
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
}
