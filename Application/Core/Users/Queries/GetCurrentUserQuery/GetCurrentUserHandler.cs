using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.User;
using Shared.Error;

namespace Application.Core.Users.Queries.GetCurrentUserQuery;
internal sealed class GetCurrentUserHandler : IQueryHandler<GetCurrentUserQuery, UserInformationDto>
{
    private readonly IRepositoryManager _repository;

    public GetCurrentUserHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Result<UserInformationDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.GetLoggedUserInformations(request.UserClaims, cancellationToken);

        return user;
    }
}
