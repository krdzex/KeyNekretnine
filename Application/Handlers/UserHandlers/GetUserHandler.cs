using Application.Queries.UserQueries;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects.User;

namespace Application.Handlers.UserHandlers;
internal sealed class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IRepositoryManager _repository;

    public GetUserHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.GetUser(request.UserId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        return user;
    }
}
