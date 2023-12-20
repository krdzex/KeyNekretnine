namespace KeyNekretnine.Api.Controllers.User;
public sealed record PasswordForgotRequest(string NewPassword, string Email, string Token);
