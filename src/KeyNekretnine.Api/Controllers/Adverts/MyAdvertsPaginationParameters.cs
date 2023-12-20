using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;
public record MyAdvertsPaginationParameters : RequestParameters
{
    public MyAdvertsPaginationParameters() => OrderBy = "createdOnDate";
    public int? Type { get; init; }
    public int? Purpose { get; init; }
    public int? Status { get; init; }
}
