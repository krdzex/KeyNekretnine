using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using Shared.DataTransferObjects.User;
using Shared.Error;

namespace Application.Core.Users.Queries.GetUserByQuery;
internal sealed class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IRepositoryManager _repository;

    public GetUserByIdHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.GetUserById(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(DomainErrors.User.UserNotFound);
        }

        return user;
    }
}