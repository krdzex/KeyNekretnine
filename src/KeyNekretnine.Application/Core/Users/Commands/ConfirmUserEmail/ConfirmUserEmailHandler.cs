using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Users.Commands.ConfirmUserEmail;
internal sealed class ConfirmUserEmailHandler : ICommandHandler<ConfirmUserEmailCommand, bool>
{
    private readonly IRepositoryManager _repository;

    public ConfirmUserEmailHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.GetUserByEmail(request.Email);

        if (user is null)
        {
            return Result.Failure<bool>(DomainErrors.User.UserNotFound);
        }

        var result = await _repository.User.ConfrimUserEmail(user, request.Email);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

            return MultipleErrorsResult<bool>.WithErrors(errors);
        }

        return true;
    }
}