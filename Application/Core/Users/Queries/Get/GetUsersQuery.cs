using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared.Pagination;

namespace KeyNekretnine.Application.Core.Users.Queries.Get;
public sealed record GetUsersQuery(
    string OrderBy,
    int PageNumber,
    int PageSize,
    string Username,
    bool? IsBanned) : IQuery<Pagination<PaginationUserResponse>>;