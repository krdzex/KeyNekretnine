using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.User;
public record UserPaginationParameters : RequestParameters
{
    public UserPaginationParameters() => OrderBy = "accountCreatedDate";
    public string? Username { get; init; }
    public bool? IsBanned { get; init; }
}
