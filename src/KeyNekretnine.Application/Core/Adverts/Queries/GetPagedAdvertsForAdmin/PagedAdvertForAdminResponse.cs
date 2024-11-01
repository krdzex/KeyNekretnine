namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertsForAdmin;
public class PagedAdvertForAdminResponse
{
    public string ReferenceId { get; set; }
    public double? Price { get; set; }
    public int NoOfBedrooms { get; set; }
    public int NoOfBathrooms { get; set; }
    public DateTime CreatedOnDate { get; set; }
    public int Purpose { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
    public string CityAndNeighborhood { get; set; }
}