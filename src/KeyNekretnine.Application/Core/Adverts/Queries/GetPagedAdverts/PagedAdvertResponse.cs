namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdverts;
public class PagedAdvertResponse
{
    public string ReferenceId { get; set; }
    public double Price { get; set; }
    public double FloorSpace { get; set; }
    public int NoOfBedrooms { get; set; }
    public int NoOfBathrooms { get; set; }
    public DateTime CreatedOnDate { get; set; }
    public string CoverImageUrl { get; set; }
    public string CityAndNeighborhood { get; set; }
    public string Address { get; set; }
    public bool IsUrgent { get; set; }
    public bool IsPremium { get; set; }
    public int Type { get; set; }
    public int Purpose { get; set; }
}
