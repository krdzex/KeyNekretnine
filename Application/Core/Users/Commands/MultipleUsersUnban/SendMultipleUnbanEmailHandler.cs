using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace KeyNekretnine.Application.Core.Users.Commands.MultipleUsersUnban;
internal sealed class SendMultipleUnbanEmailHandler : INotificationHandler<UsersUnbannedEvent>
{
    private readonly IServiceManager _serviceManager;

    public SendMultipleUnbanEmailHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task Handle(UsersUnbannedEvent request, CancellationToken cancellationToken)
    {
        foreach (var email in request.Emails)
        {
            var result = await _serviceManager.EmailService.SendUserUnbanEmail(email, cancellationToken);

            if (!result)
            {
                throw new EmailCouldntBeSentException();
            }
        }

        await Task.CompletedTask;
    }
}