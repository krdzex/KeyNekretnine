using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Shared.Error;

namespace Application.Core.Users.Commands.UnbanUser;
internal sealed class UserUnbanHandler : ICommandHandler<UnbanUserCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IPublisher _publisher;

    public UserUnbanHandler(IRepositoryManager repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Result<Unit>> Handle(UnbanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.GetUserByEmail(request.Email);

        if (user is null)
        {
            return Result.Failure<Unit>(DomainErrors.User.UserNotFound);

        }

        var unbanResult = await _repository.User.UnbanUser(user!);

        if (!unbanResult.Succeeded)
        {
            var errors = unbanResult.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

            return MultipleErrorsResult<Unit>.WithErrors(errors);
        }

        await _publisher.Publish(new UserUnbannedEvent(user!.Email), cancellationToken);

        return Unit.Value;
    }
}