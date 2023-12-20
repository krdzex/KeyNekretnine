using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserRegistration;
public sealed record UserRegistrationCommand(
    string Email,
    string Password) : ICommand;