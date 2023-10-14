namespace KeyNekretnine.Application.Core.Users.Queries.GetUserById;
public class UserResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime AccountCreatedDate { get; set; }
    public bool IsBanned { get; set; }
}
