using Application.Core.Users.Notifications.BanUser;
using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Users.Commands.BanUser;
internal sealed class BanUserHandler : ICommandHandler<BanUserCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IPublisher _publisher;

    public BanUserHandler(IRepositoryManager repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Result<Unit>> Handle(BanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.User.GetUserByEmail(request.Email);

        if (user is null)
        {
            return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
        }

        var banResult = await _repository.User.BanUser(user, request.NoOfDays);

        if (!banResult.Succeeded)
        {
            var errors = banResult.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

            return MultipleErrorsResult<Unit>.WithErrors(errors);
        }

        await _publisher.Publish(new UserBannedEvent(user.Email, request.NoOfDays), cancellationToken);

        return Unit.Value;
    }
}