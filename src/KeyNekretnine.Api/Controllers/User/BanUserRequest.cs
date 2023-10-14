namespace KeyNekretnine.Api.Controllers.User;

public sealed record BanUserRequest(string UserId, int Days);