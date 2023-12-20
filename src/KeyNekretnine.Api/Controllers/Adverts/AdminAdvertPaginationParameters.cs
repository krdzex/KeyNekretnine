using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.Adverts;

public record AdminAdvertPaginationParameters : RequestParameters
{
    public AdminAdvertPaginationParameters() => OrderBy = "createdOnDate";
    public string? ReferenceId { get; init; }
    public int? Type { get; init; }
    public int? Purpose { get; init; }
    public int? Status { get; init; }
}
