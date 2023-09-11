using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Shared;
using KeyNekretnine.Domain.Users.Events;

namespace KeyNekretnine.Domain.Users;
public class User : UserEntity
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
    public bool IsBanned { get; private set; }
    public DateTime? BanEnd { get; private set; }

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

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public void Ban(DateTime bannedUntil)
    {
        IsBanned = true;
        BanEnd = bannedUntil;

        RaiseDomainEvent(new UserBannedDomainEvent(Id));

    }

    public void UnBan()
    {
        IsBanned = false;
        BanEnd = null;

        RaiseDomainEvent(new UserUnbannedDomainEvent(Id));
    }

    public void Update(
        FirstName firstName,
        LastName lastName,
        About about)
    {
        FirstName = firstName;
        LastName = lastName;
        About = about;
    }

    public void UpdateImage(ProfileImageUrl imageUrl)
    {
        ProfileImageUrl = imageUrl;
    }
}