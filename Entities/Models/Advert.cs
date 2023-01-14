namespace Entities.Models;
public class Advert : EntityBase
{
    public double Price { get; set; }
    public string Description { get; set; }
    public int NoOfBadrooms { get; set; }
    public double FloorSpace { get; set; }
    public int NoOfBathrooms { get; set; }
    public bool HasTerrace { get; set; }
    public bool HasGarage { get; set; }
    public bool IsFurnished { get; set; }
    public bool HasWifi { get; set; }
    public bool HasElevator { get; set; }
    public int BuildingFloor { get; set; }

    public int AdvertStatusId { get; set; } = 2;
    public AdvertStatus AdvertStatus { get; set; }

    public int AdvertPurposeId { get; set; }
    public AdvertPurpose AdvertPurpose { get; set; }

    public int AdvertTypeId { get; set; }
    public AdvertType AdvertType { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string UserId { get; set; }
    public User User { get; set; }

    public List<Image> Images { get; set; }

    public string CoverImageUrl { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public Neighborhood Neighborhood { get; set; }
    public int NeighborhoodId { get; set; }

    public int? YearOfBuildingCreated { get; set; }
    public string Street { get; set; }
}
