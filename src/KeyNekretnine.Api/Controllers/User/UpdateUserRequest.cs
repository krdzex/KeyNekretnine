namespace KeyNekretnine.Api.Controllers.User;

public sealed record UpdateUserRequest(
    string About,
    string FirstName,
    string LastName,
    string PhoneNumber,
    IFormFile Image);
