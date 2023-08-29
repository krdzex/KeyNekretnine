using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Auth;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Auth.Queries.UserLogin;
public sealed record LoginUserQuery(UserForAuthenticationDto LoginUser) : IQuery<TokenRequest>;