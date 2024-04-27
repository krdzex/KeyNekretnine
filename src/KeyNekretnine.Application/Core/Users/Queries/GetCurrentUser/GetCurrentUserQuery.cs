using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Queries.GetCurrentUser;
public sealed record GetCurrentUserQuery() : IQuery<CurrentUserResponse>;