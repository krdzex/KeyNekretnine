using Application.Notifications.AuthNotification;
using MediatR;
using Service.Contracts;

namespace Application.Handlers.UserHandlers;
internal sealed class RegisterUserHandler : INotificationHandler<UserSignupNotification>
{
    private readonly IServiceManager _service;

    public RegisterUserHandler(IServiceManager service)
    {
        _service = service;
    }

    public async Task Handle(UserSignupNotification notification, CancellationToken cancellationToken)
    {
        await _service.AuthenticationService.RegisterUser(notification.RegistrationUser);
    }
}
