using Application.Notifications.AdvertNotifications;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Handlers.EmailHandlers;
internal sealed class ApproveAdvertEmailHandler : INotificationHandler<ApproveAdvertNotification>
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;

    public ApproveAdvertEmailHandler(IServiceManager serviceManager, IRepositoryManager repositoryManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
    }

    public async Task Handle(ApproveAdvertNotification notification, CancellationToken cancellationToken)
    {
        var userEmail = await _repositoryManager.Advert.GetUserEmailFromAdvertId(notification.AdvertId, cancellationToken);

        var sendStatus = await _serviceManager.EmailService.SendApproveAdvertEmail(userEmail, notification.AdvertId, cancellationToken);

        if (!sendStatus)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}

