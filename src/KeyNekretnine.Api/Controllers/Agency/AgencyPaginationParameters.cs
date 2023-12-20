using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Agency;

public record AgencyPaginationParameters : RequestParameters
{
    public AgencyPaginationParameters() => OrderBy = "createdDate";
    public string Name { get; init; }
}
