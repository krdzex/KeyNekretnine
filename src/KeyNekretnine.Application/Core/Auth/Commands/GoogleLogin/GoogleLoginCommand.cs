using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Auth.Commands.GoogleLogin;
public sealed record GoogleLoginCommand(string GoogleIdToken) : ICommand<TokenResponse>;