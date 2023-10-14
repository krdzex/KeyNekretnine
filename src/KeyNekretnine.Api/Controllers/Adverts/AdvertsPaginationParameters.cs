using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;
public class AdvertsPaginationParameters : RequestParameters
{
    public AdvertsPaginationParameters() => OrderBy = "createdOnDate";
    public int MinPrice { get; set; } = 0;
    public int MaxPrice { get; set; } = int.MaxValue;
    public int MinFloorSpace { get; set; } = 0;
    public int MaxFloorSpace { get; set; } = int.MaxValue;
    public List<int> NoOfBedrooms { get; set; }
    public List<int> NoOfBathrooms { get; set; }
    public List<int> Types { get; set; }
    public List<int> Purposes { get; set; }
    public int? CityId { get; set; }
    public List<int> Neighborhoods { get; set; }
    public bool IsEmergency { get; set; }
    public bool IsUnderConstruction { get; set; }
    public bool IsFurnished { get; set; }
}