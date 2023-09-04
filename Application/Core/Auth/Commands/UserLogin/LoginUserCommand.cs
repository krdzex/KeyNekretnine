using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserLogin;
public sealed record LoginUserCommand(string Email, string Password) : ICommand<TokenResponse>;