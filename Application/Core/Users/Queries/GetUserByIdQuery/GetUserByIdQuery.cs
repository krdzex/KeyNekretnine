using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.User;

namespace KeyNekretnine.Application.Core.Users.Queries.GetUserByIdQuery;
public sealed record GetUserByIdQuery(string UserId) : IQuery<UserDto>;