using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Queries.GetByIdQuery;
public sealed record GetUserByIdQuery(string UserId) : IQuery<UserResponse>;