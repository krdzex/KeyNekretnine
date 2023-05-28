using MediatR;
using Shared.RequestFeatures;

namespace Application.Commands.AuthCommands;
public sealed record FacebookLoginCommand(string FbAccessToken) : IRequest<TokenRequest>;
