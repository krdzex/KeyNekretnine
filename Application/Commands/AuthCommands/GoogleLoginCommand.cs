using MediatR;
using Shared.DataTransferObjects.Auth;
using Shared.RequestFeatures;

namespace Application.Commands.AuthCommands;
public sealed record GoogleLoginCommand(GoogleLoginDto GoogleLoginDto) : IRequest<TokenRequest>;