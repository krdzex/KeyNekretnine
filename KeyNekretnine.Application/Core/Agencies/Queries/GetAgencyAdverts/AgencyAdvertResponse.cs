namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAdverts;
public sealed class AgencyAdvertResponse
{
    public string Location { get; set; }
    public double Price { get; set; }
    public string CoverImageUrl { get; set; }
    public int NoOfBathrooms { get; set; }
    public int NoOfBedrooms { get; set; }
    public double FloorSpace { get; set; }
    public DateTime CreatedOnDate { get; set; }
    public string Address { get; set; }
    public bool IsUrgent { get; set; }
    public bool IsUnderConstruction { get; set; }
    public bool IsFurnished { get; set; }
    public int Type { get; set; }
    public int Purpose { get; set; }
}
