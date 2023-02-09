using MediatR;
using Shared.DataTransferObjects.User;
using System.Security.Claims;

namespace Application.Queries.UserQueries;
public sealed record GetCurrentUserQuery(IEnumerable<Claim> UserClaims) : IRequest<UserInformationDto>;

