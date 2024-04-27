using KeyNekretnine.Application.Abstraction.Messaging;
using Microsoft.AspNetCore.Http;

namespace KeyNekretnine.Application.Core.Users.Commands.UpdateUser;
public sealed record UpdateUserCommand(
    string About,
    string FirstName,
    string LastName,
    IFormFile Image) : ICommand;