using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Auth;
using Shared.RequestFeatures;

namespace Application.Core.Auth.Commands.GoogleLogin;
public sealed record GoogleLoginCommand(GoogleLoginDto GoogleLoginDto) : ICommand<TokenRequest>;