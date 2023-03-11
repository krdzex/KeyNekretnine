using Application.Queries.UserQueries;
using Contracts;
using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;

namespace Application.Handlers.UserHandlers;
internal sealed class GetUsersHandler : IRequestHandler<GetUsersQuery, Pagination<UserForListDto>>
{
    private readonly IRepositoryManager _repository;

    public GetUsersHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Pagination<UserForListDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.User.GetUsers(request.UserParameters, cancellationToken);

        return users;
    }
}
