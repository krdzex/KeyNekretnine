using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Auth.Commands.RefreshTokens;
public sealed record RefreshTokensCommand(string AccessToken, string RefreshToken) : ICommand<RefreshTokenResponse>;