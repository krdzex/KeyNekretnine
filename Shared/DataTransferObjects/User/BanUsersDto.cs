namespace Shared.DataTransferObjects.User;
public class BanUsersDto
{
    public IEnumerable<string> UserIds { get; set; }
    public int Days { get; set; }
}
