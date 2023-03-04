using MediatR;
using Shared.DataTransferObjects.User;

namespace Application.Queries.UserQueries;
public sealed record GetUserQuery(string UserId) : IRequest<UserDto>;


