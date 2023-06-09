using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Core.Users.Commands.UnbanUser;
internal sealed class SendUnbanEmailHandler : INotificationHandler<UserUnbannedEvent>
{
    private readonly IServiceManager _serviceManager;

    public SendUnbanEmailHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    public async Task Handle(UserUnbannedEvent request, CancellationToken cancellationToken)
    {

        var result = await _serviceManager.EmailService.SendUserUnbanEmail(request.Email, cancellationToken);

        if (!result)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}