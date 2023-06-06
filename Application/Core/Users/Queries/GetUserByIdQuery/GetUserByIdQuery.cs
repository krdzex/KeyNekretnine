using Application.Abstraction.Messaging;
using Shared.DataTransferObjects.User;

namespace Application.Core.Users.Queries.GetUserByQuery;
public sealed record GetUserByIdQuery(string UserId) : IQuery<UserDto>;