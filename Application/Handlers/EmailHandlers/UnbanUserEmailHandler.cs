using Application.Notifications.UserNotifications;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Handlers.EmailHandlers;
internal sealed class UnbanUserEmailHandler : INotificationHandler<UnbanUserNotification>
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;

    public UnbanUserEmailHandler(IServiceManager serviceManager, IRepositoryManager repositoryManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
    }
    public async Task Handle(UnbanUserNotification request, CancellationToken cancellationToken)
    {
        var userEmail = await _repositoryManager.User.GetEmailFromUserId(request.UserId);

        var result = await _serviceManager.EmailService.SendUserUnbanEmail(userEmail);

        if (!result)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}
