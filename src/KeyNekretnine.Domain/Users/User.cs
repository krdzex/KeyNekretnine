﻿using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Shared;
using KeyNekretnine.Domain.UserAdvertFavorites;
using KeyNekretnine.Domain.Users.Events;

namespace KeyNekretnine.Domain.Users;
public class User : UserEntity
{
    private readonly List<UserAdvertFavorite> _favorites = new();

    private User(
        FirstName firstName,
        LastName lastName,
        string email,
        string userName,
        DateTime accountCreatedDate,
        bool isAgency)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
        AccountCreatedDate = accountCreatedDate;
        IsAgency = isAgency;
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
    public bool IsAgency { get; private set; }
    public IReadOnlyCollection<UserAdvertFavorite> UserAdvertFavorites => _favorites;

    public static User Create(
        FirstName firstName,
        LastName lastName,
        string email,
        string userName,
        DateTime createdDate,
        bool isAgency)
    {
        var user = new User(
            firstName,
            lastName,
            email,
            userName,
            createdDate,
            isAgency);

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

    public Result AddAdvertToFavorites(Guid advertId, DateTime createdOnTime)
    {
        if (_favorites.Any(f => f.AdvertId == advertId))
        {
            return Result.Failure(UserErrors.AlreadyFavorite);
        }

        var favorite = new UserAdvertFavorite
        {
            UserId = Id,
            AdvertId = advertId,
            CreatedFavoriteDate = createdOnTime
        };
        _favorites.Add(favorite);

        return Result.Success();
    }

    public Result RemoveAdvertFromFavorites(Guid advertId)
    {
        var favorite = _favorites.FirstOrDefault(f => f.AdvertId == advertId);

        if (favorite == null)
        {
            return Result.Failure(UserErrors.NotFavorite);
        }

        _favorites.Remove(favorite);

        return Result.Success();
    }
}