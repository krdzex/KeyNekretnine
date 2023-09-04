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

    public static Error Banned(DateTime? banEndDate) => new(
        "User.Banned",
        $"USer is banned until {banEndDate}");
}
