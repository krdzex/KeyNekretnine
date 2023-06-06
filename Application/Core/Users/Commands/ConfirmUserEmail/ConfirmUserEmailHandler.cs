using Application.Abstraction.Messaging;
using Application.Core.Users.Queries.ConfirmEmailQuery;
using Contracts;
using Entities.DomainErrors;
using Shared.Error;

namespace Application.Core.Users.Queries.ConfirmUserEmail;
internal sealed class ConfirmUserEmailHandler : ICommandHandler<ConfirmUserEmailCommand, bool>
{
    private readonly IRepositoryManager _repository;

    public ConfirmUserEmailHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Result<bool>> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.FindUserByEmail(request.Email);

        if (user is null)
        {
            return Result.Failure<bool>(DomainErrors.User.UserNotFound);
        }

        var result = await _repository.User.ConfrimUserEmail(user, request.Email);

        return result.Succeeded;
    }
}
