namespace Entities.Models;
public class Advert : EntityBase
{
    public double Price { get; set; }
    public string DescriptionSr { get; set; }
    public string DescriptionEn { get; set; }
    public int NoOfBedrooms { get; set; }
    public double FloorSpace { get; set; }
    public int NoOfBathrooms { get; set; }
    public bool HasTerrace { get; set; }
    public bool HasGarage { get; set; }
    public bool IsFurnished { get; set; }
    public bool HasWifi { get; set; }
    public bool HasElevator { get; set; }
    public int BuildingFloor { get; set; }

    public int StatusId { get; set; } = 2;
    public AdvertStatus Status { get; set; }

    public int PurposeId { get; set; }
    public AdvertPurpose Purpose { get; set; }

    public int TypeId { get; set; }
    public AdvertType Type { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string UserId { get; set; }
    public User User { get; set; }


    public int? ImaginaryAgentId { get; set; }
    public ImaginaryAgent ImaginaryAgent { get; set; }

    public List<Image> Images { get; set; }
    public string CoverImageUrl { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Neighborhood Neighborhood { get; set; }
    public int NeighborhoodId { get; set; }

    public int? YearOfBuildingCreated { get; set; }
    public string Street { get; set; }
    public List<TemporeryImageData> TemporeryImageDatas { get; set; }
    public string ReferenceId { get; set; }
    public IEnumerable<UserAdvertFavorite> UserAdvertFavorites { get; set; }
    public IEnumerable<UserAdvertReport> UserAdvertReports { get; set; }
    public List<AdvertFeature> Features { get; set; }
    public bool IsEmergency { get; set; }
    public bool IsUnderConstruction { get; set; }
}