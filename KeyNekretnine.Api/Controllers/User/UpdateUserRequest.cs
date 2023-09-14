namespace KeyNekretnine.Api.Controllers.User;

public sealed record UpdateUserRequest(
    string About,
    string FirstName,
    string LastName,
    IFormFile Image);
