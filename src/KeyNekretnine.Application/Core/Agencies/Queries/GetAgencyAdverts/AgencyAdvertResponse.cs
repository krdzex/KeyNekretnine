namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAdverts;
public sealed class AgencyAdvertResponse
{
    public string ReferenceId { get; init; }
    public string CityAndNeighborhood { get; init; }
    public double Price { get; init; }
    public string CoverImageUrl { get; init; }
    public int NoOfBathrooms { get; init; }
    public int NoOfBedrooms { get; init; }
    public double FloorSpace { get; init; }
    public string Address { get; init; }
    public bool IsUrgent { get; init; }
    public bool IsPremium { get; init; }
    public int Type { get; init; }
    public int Purpose { get; init; }
    public DateTime CreatedOnDate { get; init; }
}
