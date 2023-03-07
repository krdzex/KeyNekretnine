using Application.Notifications.AdvertNotifications;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class ApproveAdvertHandler : INotificationHandler<ApproveAdvertNotification>
{
    private readonly IRepositoryManager _repository;

    public ApproveAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task Handle(ApproveAdvertNotification notification, CancellationToken cancellationToken)
    {

        var advertExist = await _repository.Advert.ChackIfAdvertExist(notification.AdvertId, cancellationToken);

        if (!advertExist)
        {
            throw new AdvertNotFoundException(notification.AdvertId);
        }

        await _repository.Advert.ApproveAdvert(notification.AdvertId, cancellationToken);

        await Task.CompletedTask;
    }
}

