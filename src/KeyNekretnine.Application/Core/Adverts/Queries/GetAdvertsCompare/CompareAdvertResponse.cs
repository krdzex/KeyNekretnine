using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertsCompare;
public class CompareAdvertResponse
{
    public string ReferenceId { get; set; }
    public double Price { get; set; }
    public string CoverImageUrl { get; set; }
    public int NoOfBathrooms { get; set; }
    public int NoOfBedrooms { get; set; }
    public double FloorSpace { get; set; }
    public bool HasElevator { get; set; }
    public bool HasGarage { get; set; }
    public bool HasTerrace { get; set; }
    public bool HasWifi { get; set; }
    public int YearOfBuildingCreated { get; set; }
    public int BuildingFloor { get; set; }
    public string Creator { get; set; }
    public AdvertLocationResponse Location { get; set; }
    public int Type { get; set; }
    public int Purpose { get; set; }
}
