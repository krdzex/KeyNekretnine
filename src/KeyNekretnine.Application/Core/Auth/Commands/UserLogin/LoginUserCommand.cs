using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserLogin;
public sealed record LoginUserCommand(string Email, string Password) : ICommand<TokenResponse>;