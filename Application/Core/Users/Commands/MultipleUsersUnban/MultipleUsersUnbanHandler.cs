using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Shared.Error;
namespace Application.Core.Users.Commands.MultipleUsersUnban;
internal sealed class MultipleUsersUnbanHandler : ICommandHandler<MultipleUsersUnbanCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IPublisher _publisher;

    public MultipleUsersUnbanHandler(IRepositoryManager repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }
    public async Task<Result<Unit>> Handle(MultipleUsersUnbanCommand request, CancellationToken cancellationToken)
    {
        foreach (var email in request.Emails)
        {
            var user = await _repository.User.GetUserByEmail(email);

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
        }

        await _publisher.Publish(new UsersUnbannedEvent(request.Emails), cancellationToken);
        return Unit.Value;
    }
}

