using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;

namespace Application.Queries.UserQueries;
public sealed record GetBannedUsersQuery(UserParameters UserParameters) : IRequest<Pagination<UserForListDto>>;

