using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Tokens.Commands.RefreshTokens;
public sealed record RefreshTokensCommand(TokenRequest OldTokens) : ICommand<TokenRequest>;