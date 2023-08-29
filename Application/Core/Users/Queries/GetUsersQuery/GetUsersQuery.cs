using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Users.Queries.GetUsersQuery;
public sealed record GetUsersQuery(UserParameters UserParameters) : IQuery<Pagination<UserForListDto>>;