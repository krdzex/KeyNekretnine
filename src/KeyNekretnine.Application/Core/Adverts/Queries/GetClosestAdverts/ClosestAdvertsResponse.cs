namespace KeyNekretnine.Application.Core.Adverts.Queries.GetClosestAdverts;
public class ClosestAdvertsResponse
{
    public string ReferenceId { get; init; }
    public double? Price { get; init; }
    public double FloorSpace { get; init; }
    public int NoOfBedrooms { get; init; }
    public int NoOfBathrooms { get; init; }
    public DateTime CreatedOnDate { get; init; }
    public string CoverImageUrl { get; init; }
    public string CityAndNeighborhood { get; init; }
    public string Address { get; init; }
    public bool IsUrgent { get; init; }
    public bool IsPremium { get; init; }
    public int Type { get; init; }
    public int Purpose { get; init; }
    public int DistanceMeters { get; init; }
}
