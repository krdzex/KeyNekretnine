using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Shared.Error;
namespace Application.Core.Users.Notifications.MultipleUserBan
{
    internal sealed class MultipleUsersBanHandler : ICommandHandler<MultipleUsersBanCommand, Unit>
    {
        private readonly IRepositoryManager _repository;
        private readonly IPublisher _publisher;
        public MultipleUsersBanHandler(IRepositoryManager repository, IPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }
        public async Task<Result<Unit>> Handle(MultipleUsersBanCommand request, CancellationToken cancellationToken)
        {
            foreach (var email in request.Emails)
            {
                var user = await _repository.User.GetUserByEmail(email);

                if (user is null)
                {
                    return Result.Failure<Unit>(DomainErrors.User.UserNotFound);

                }

                var updateResult = await _repository.User.BanUser(user, request.NoOfDays);

                if (!updateResult.Succeeded)
                {
                    //Need to change
                    return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
                }
            }

            await _publisher.Publish(new UsersBannedEvent(request.Emails, request.NoOfDays), cancellationToken);

            return Unit.Value;
        }
    }

}
