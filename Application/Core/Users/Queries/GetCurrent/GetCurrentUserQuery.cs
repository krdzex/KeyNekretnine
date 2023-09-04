using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Queries.GetCurrent;
public sealed record GetCurrentUserQuery(string UserId) : IQuery<CurrentUserResponse>;