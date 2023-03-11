using Application.Notifications.UserNotifications;
using Contracts;
using MediatR;

namespace Application.Handlers.UserHandlers;
internal sealed class UserUnbanHandler : INotificationHandler<UnbanUserNotification>
{
    private readonly IRepositoryManager _repository;

    public UserUnbanHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task Handle(UnbanUserNotification request, CancellationToken cancellationToken)
    {
        await _repository.User.UnbanUser(request.UserId, cancellationToken);

        await Task.CompletedTask;
    }
}
