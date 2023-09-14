using KeyNekretnine.Api.Controllers.Shared;

namespace KeyNekretnine.Api.Controllers.User;
public class UserPaginationParameters : RequestParameters
{
    public UserPaginationParameters() => OrderBy = "account_created_date";
    public string? Username { get; set; }
    public bool? IsBanned { get; set; }
}
