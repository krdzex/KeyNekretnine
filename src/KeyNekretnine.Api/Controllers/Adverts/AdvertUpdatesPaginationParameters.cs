using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;
public record AdvertUpdatesPaginationParameters : RequestParameters
{
    public AdvertUpdatesPaginationParameters() => OrderBy = "createdOnDate";
    public string ReferenceId { get; init; }
    public int? UpdateType { get; set; }
}
