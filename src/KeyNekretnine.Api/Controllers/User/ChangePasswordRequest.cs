namespace KeyNekretnine.Api.Controllers.User;
public sealed record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword);
