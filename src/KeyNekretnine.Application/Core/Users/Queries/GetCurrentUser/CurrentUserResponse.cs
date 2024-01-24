namespace KeyNekretnine.Application.Core.Users.Queries.GetCurrentUser;
public class CurrentUserResponse
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string ProfileImageUrl { get; init; }
    public string Email { get; init; }
    public bool IsAgency { get; init; }
    public int AgencyId { get; set; }
    public string AgencyName { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}

