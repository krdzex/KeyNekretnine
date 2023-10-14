using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Agency;

public class AgencyPaginationParameters : RequestParameters
{
    public AgencyPaginationParameters() => OrderBy = "createdDate";
    public string? Name { get; set; }
}
