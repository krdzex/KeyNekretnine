using Application.Abstraction.Messaging;
using Shared.RequestFeatures;

namespace Application.Core.Tokens.Commands.RefreshTokens;
public sealed record RefreshTokensCommand(TokenRequest OldTokens) : ICommand<TokenRequest>;