namespace KeyNekretnine.Application.Core.Users.Queries.GetUsers;
public class PaginationUserResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public bool IsBanned { get; set; }
    public DateTime AccountCreatedDate { get; set; }
    public int NumAdverts { get; set; }
}
