using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;

public class AdminAdvertPaginationParameters : RequestParameters
{
    public AdminAdvertPaginationParameters() => OrderBy = "createdOnDate";
    public string? ReferenceId { get; set; }
    public int? Type { get; set; }
    public int? Purpose { get; set; }
    public int? Status { get; set; }
}
