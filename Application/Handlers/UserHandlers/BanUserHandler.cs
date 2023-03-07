using Application.Notifications.UserNotifications;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers;

internal sealed class BanUserHandler : INotificationHandler<BanUserNotification>
{
    private readonly IRepositoryManager _repository;

    public BanUserHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task Handle(BanUserNotification request, CancellationToken cancellationToken)
    {
        await _repository.User.BanUser(request.UserId, request.NoOfDays);

        await Task.CompletedTask;
    }
}
