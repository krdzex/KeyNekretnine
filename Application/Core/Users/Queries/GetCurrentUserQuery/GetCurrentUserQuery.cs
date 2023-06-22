using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.User;
using System.Security.Claims;

namespace Application.Core.Users.Queries.GetCurrentUserQuery;
public sealed record GetCurrentUserQuery(IEnumerable<Claim> UserClaims) : IQuery<UserInformationDto>;