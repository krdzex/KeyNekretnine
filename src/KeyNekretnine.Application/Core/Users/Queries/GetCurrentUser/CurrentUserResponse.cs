namespace KeyNekretnine.Application.Core.Users.Queries.GetCurrentUser;
public class CurrentUserResponse
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string ProfileImageUrl { get; init; }
    public string Email { get; init; }
    public bool IsAgency { get; init; }
    public Guid AgencyId { get; init; }
    public string AgencyName { get; init; }
    public List<string> Roles { get; init; } = new List<string>();
}

