using Application.Notifications.UserNotifications;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Handlers.EmailHandlers;
internal sealed class MultipleUnbanUserEmailHandler : INotificationHandler<MultipleUnbanUserNotification>
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;

    public MultipleUnbanUserEmailHandler(IServiceManager serviceManager, IRepositoryManager repositoryManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
    }
    public async Task Handle(MultipleUnbanUserNotification request, CancellationToken cancellationToken)
    {
        foreach (var userId in request.UserIds)
        {
            var userEmail = await _repositoryManager.User.GetEmailFromUserId(userId, cancellationToken);

            var result = await _serviceManager.EmailService.SendUserUnbanEmail(userEmail);

            if (!result)
            {
                throw new EmailCouldntBeSentException();
            }
        }

        await Task.CompletedTask;
    }
}
