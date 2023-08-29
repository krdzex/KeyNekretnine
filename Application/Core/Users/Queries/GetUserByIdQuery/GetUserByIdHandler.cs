using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.User;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Users.Queries.GetUserByIdQuery;
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