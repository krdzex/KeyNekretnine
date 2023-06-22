using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using Shared.DataTransferObjects.User;
using Shared.Error;
using System.Security.Claims;

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
        var email = request.UserClaims.FirstOrDefault(q => q.Type == ClaimTypes.Email)!.Value;

        var user = await _repository.User.GetLoggedUserInformationsByEmail(email, cancellationToken);

        if (user.Email is null)
        {
            return Result.Failure<UserInformationDto>(DomainErrors.User.UserNotFound);
        }

        var roles = request.UserClaims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value);

        user.Roles = roles;

        return user;
    }
}