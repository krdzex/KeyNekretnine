namespace KeyNekretnine.Application.Core.Users.Queries.GetAboutUser;
public class AboutUserResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime AccountCreatedDate { get; set; }
    public string ProfileImageUrl { get; set; }
    public string About { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsAgency { get; set; }
    public string AgencyName { get; set; }
    public bool CanChangePassword { get; set; }
}