namespace Shared.DataTransferObjects.Auth;
public class UserForRegistrationDto : UserForAuthenticationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
}