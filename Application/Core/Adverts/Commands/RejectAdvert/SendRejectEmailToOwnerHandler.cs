using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
internal sealed class SendRejectEmailToOwnerHandler : INotificationHandler<AdvertRejectedEvent>
{
    private readonly IServiceManager _serviceManager;

    public SendRejectEmailToOwnerHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(AdvertRejectedEvent notification, CancellationToken cancellationToken)
    {

        var sendStatus = await _serviceManager.EmailService.SendDeclineAdvertEmail(notification.Email, notification.AdvertId, cancellationToken);

        if (!sendStatus)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}