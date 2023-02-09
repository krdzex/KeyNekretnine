namespace Shared.DataTransferObjects.User;
public class UserInformationDto
{
    public string Email { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
