using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.UserAdvertFavorites;
using KeyNekretnine.Domain.UserAdvertReports;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Domain.Users;
public class User : IdentityUser
{
    private User(FirstName firstName, LastName lastName, string email, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
    }

    private User()
    {
    }

    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }
    public DateTime AccountCreatedDate { get; set; } = DateTime.Now;
    public ProfileImageUrl ProfileImageUrl { get; set; }
    public About About { get; set; }
    public List<Advert> Adverts { get; set; }
    public bool IsBanned { get; set; }
    public DateTime? BanEnd { get; set; }
    public IEnumerable<UserAdvertFavorite> UserAdvertFavorites { get; set; }
    public IEnumerable<UserAdvertReport> UserAdvertReports { get; set; }

    public static User Create(FirstName firstName, LastName lastName, string email, string userName)
    {
        var user = new User(firstName, lastName, email, userName);

        return user;
    }
}