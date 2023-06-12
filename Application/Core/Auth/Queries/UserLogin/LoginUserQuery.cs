using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Auth;
using Shared.RequestFeatures;

namespace Application.Core.Auth.Queries.UserLogin;
public sealed record LoginUserQuery(UserForAuthenticationDto LoginUser) : IQuery<TokenRequest>;