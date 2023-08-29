using Contracts;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Users.Queries.GetUsersQuery;
internal sealed class GetUsersHandler : IQueryHandler<GetUsersQuery, Pagination<UserForListDto>>
{
    private readonly IRepositoryManager _repository;

    public GetUsersHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Pagination<UserForListDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.User.GetUsers(request.UserParameters, cancellationToken);

        return users;
    }
}