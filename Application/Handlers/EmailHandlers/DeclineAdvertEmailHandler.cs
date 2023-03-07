using Application.Notifications.AdvertNotifications;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Handlers.EmailHandlers;
internal sealed class DeclineAdvertEmailHandler : INotificationHandler<DeclineAdvertNotification>
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;

    public DeclineAdvertEmailHandler(IServiceManager serviceManager, IRepositoryManager repositoryManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
    }

    public async Task Handle(DeclineAdvertNotification notification, CancellationToken cancellationToken)
    {
        var userEmail = await _repositoryManager.Advert.GetUserEmailFromAdvertId(notification.AdvertId, cancellationToken);

        var sendStatus = await _serviceManager.EmailService.SendDeclineAdvertEmail(userEmail, notification.AdvertId);

        if (!sendStatus)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}
