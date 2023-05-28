using Application.Notifications.AuthNotification;
using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Handlers.EmailHandlers;
internal sealed class WelcomeEmailHandler : INotificationHandler<UserSignupNotification>
{
    private readonly IServiceManager _serviceManager;
    public WelcomeEmailHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(UserSignupNotification notification, CancellationToken cancellationToken)
    {
        var sendStatus = await _serviceManager.EmailService.SendWelcomeEmail(notification.RegistrationUser.Email);

        if (!sendStatus)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}
