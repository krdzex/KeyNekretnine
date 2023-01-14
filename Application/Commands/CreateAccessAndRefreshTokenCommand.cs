using MediatR;
using Shared.RequestFeatures;

namespace Application.Commands;
public sealed record CreateAccessAndRefreshTokenCommand(TokenRequest OldTokens) : IRequest<TokenRequest>;

