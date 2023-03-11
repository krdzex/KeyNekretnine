using Application.Notifications.UserNotifications;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers;
internal sealed class MultipleUnbanUserHandler : INotificationHandler<MultipleUnbanUserNotification>
{
    private readonly IRepositoryManager _repository;

    public MultipleUnbanUserHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task Handle(MultipleUnbanUserNotification request, CancellationToken cancellationToken)
    {
        foreach (var userId in request.UserIds)
        {
            await _repository.User.UnbanUser(userId, cancellationToken);
        }

        await Task.CompletedTask;
    }
}

