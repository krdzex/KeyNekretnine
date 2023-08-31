using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Agency;

public class AgencyPaginationParameters : RequestParameters
{
    public AgencyPaginationParameters() => OrderBy = "created_date";
    public string? Name { get; set; }
}
