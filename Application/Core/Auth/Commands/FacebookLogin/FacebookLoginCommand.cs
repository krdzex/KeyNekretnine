using Application.Abstraction.Messaging;
using Shared.RequestFeatures;

namespace Application.Core.Auth.Commands.FacebookLogin;
public sealed record FacebookLoginCommand(string FbAccessToken) : ICommand<TokenRequest>;