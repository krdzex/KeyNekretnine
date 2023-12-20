namespace KeyNekretnine.Api.Controllers.Authentication;

public sealed record RegisterUserRequest(
    string Email,
    string Password);
