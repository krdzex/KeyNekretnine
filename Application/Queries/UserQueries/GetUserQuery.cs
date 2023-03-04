using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;

namespace Application.Queries.UserQueries;
public sealed record GetUserQuery(string UserId) : IRequest<Pagination<UserForListDto>>;


