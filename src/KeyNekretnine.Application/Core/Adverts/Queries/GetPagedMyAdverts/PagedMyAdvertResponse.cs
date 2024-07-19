using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetPagedMyAdverts;
public class PagedMyAdvertResponse
{
    public string ReferenceId { get; set; }
    public int Type { get; set; }
    public int Purpose { get; set; }
    public int Status { get; set; }
    public AdvertDescriptionResponse Description { get; set; }
    public string CoverImageUrl { get; set; }
    public string CityAndNeighborhood { get; set; }
    public bool PendingUpdates { get; set; }
    public DateTime CreatedOnDate { get; set; }
    public DateTime? UpdatedOnDate { get; set; }
}