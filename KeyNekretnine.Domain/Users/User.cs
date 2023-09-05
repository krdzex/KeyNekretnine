using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.UserAdvertFavorites;
using KeyNekretnine.Domain.UserAdvertReports;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Domain.Users;
public class User : IdentityUser
{
    private User(FirstName firstName, LastName lastName, string email, string userName, DateTime accountCreatedDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
        AccountCreatedDate = accountCreatedDate;
    }

    private User()
    {
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateTime AccountCreatedDate { get; private set; }
    public ProfileImageUrl ProfileImageUrl { get; private set; }
    public About About { get; private set; }
    public List<Advert> Adverts { get; private set; }
    public bool IsBanned { get; private set; }
    public DateTime? BanEnd { get; private set; }
    public IEnumerable<UserAdvertFavorite> UserAdvertFavorites { get; private set; }
    public IEnumerable<UserAdvertReport> UserAdvertReports { get; private set; }

    public static User Create(
        FirstName firstName,
        LastName lastName,
        string email,
        string userName,
        DateTime createdDate)
    {
        var user = new User(
            firstName,
            lastName,
            email,
            userName,
            createdDate);

        return user;
    }

    public void Ban(DateTime bannedUntil)
    {
        IsBanned = true;
        BanEnd = bannedUntil;
    }

    public void UnBan()
    {
        IsBanned = false;
        BanEnd = null;
    }
}