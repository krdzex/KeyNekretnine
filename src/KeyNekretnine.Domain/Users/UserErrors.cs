using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Users;
public static class UserErrors
{
    public static Error NotFound = new(
        "User.NotFound",
        "User is not found");

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");

    public static Error InvalidToken = new(
        "User.Invalid token",
        "Provided token is not valid");

    public static Error Banned(DateTime? banEndDate) => new(
        "User.Banned",
        $"User is banned until {banEndDate}");

    public static Error AlreadyFavorite => new(
        "User.AlreadyFavorite",
        $"Advert is already favorite");

    public static Error NotFavorite => new(
        "User.NotFavorite",
        $"Advert is not favorite");
}
