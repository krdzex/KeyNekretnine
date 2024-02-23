namespace KeyNekretnine.Application.Core.Users.Queries.GetAboutUser;
public class AboutUserResponse
{
    public string Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public DateTime AccountCreatedDate { get; init; }
    public string ProfileImageUrl { get; init; }
    public string About { get; init; }
    public bool IsEmailConfirmed { get; init; }
    public string PhoneNumber { get; init; }
    public bool IsAgency { get; init; }
    public string AgencyName { get; init; }
    public bool CanChangePassword { get; init; }
}