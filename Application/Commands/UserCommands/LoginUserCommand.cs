using MediatR;
using Shared.DataTransferObjects.Auth;
using Shared.RequestFeatures;

namespace Application.Commands.UserCommands;
public sealed record LoginUserCommand(UserForAuthenticationDto LoginUser) : IRequest<TokenRequest>;

