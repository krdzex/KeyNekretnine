using Application.Notifications.UserNotifications;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers
{
    internal sealed class MultipleBanUserHandler : INotificationHandler<MultipleBanUserNotification>
    {
        private readonly IRepositoryManager _repository;

        public MultipleBanUserHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task Handle(MultipleBanUserNotification request, CancellationToken cancellationToken)
        {
            foreach (var userId in request.UserIds)
            {
                await _repository.User.BanUser(userId, request.NoOfDays, cancellationToken);
            }

            await Task.CompletedTask;
        }
    }

}
