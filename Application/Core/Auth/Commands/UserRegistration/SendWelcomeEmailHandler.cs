using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Core.Auth.Commands.UserRegistration;
internal sealed class SendWelcomeEmailHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IServiceManager _serviceManager;
    public SendWelcomeEmailHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var sendResult = await _serviceManager.EmailService.SendWelcomeEmail(notification.Email, cancellationToken);

        if (!sendResult)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}
