using Application.Queries.UserQueries;
using Contracts;
using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;

namespace Application.Handlers.UserHandlers
{
    internal class GetBannedUsersHandler
    {
    }
}
internal sealed class GetBannedUsersHandler : IRequestHandler<GetBannedUsersQuery, Pagination<UserForListDto>>
{
    private readonly IRepositoryManager _repository;

    public GetBannedUsersHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Pagination<UserForListDto>> Handle(GetBannedUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.User.GetBannedUsers(request.UserParameters);

        return users;
    }
}