using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Core.Adverts.Commands.ApproveAdvert;
internal sealed class SendApproveEmailToOwnerHandler : INotificationHandler<AdvertApprovedEvent>
{
    private readonly IServiceManager _serviceManager;

    public SendApproveEmailToOwnerHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(AdvertApprovedEvent notification, CancellationToken cancellationToken)
    {

        var sendStatus = await _serviceManager.EmailService.SendApproveAdvertEmail(notification.Email, notification.AdvertId, cancellationToken);

        if (!sendStatus)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}