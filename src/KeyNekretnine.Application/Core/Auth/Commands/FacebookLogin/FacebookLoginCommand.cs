using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Auth.Commands.FacebookLogin;
public sealed record FacebookLoginCommand(string FbAccessToken) : ICommand<TokenRequest>;