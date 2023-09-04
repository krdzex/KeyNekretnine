using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserRegistration;
public sealed record UserRegistrationCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password) : ICommand;