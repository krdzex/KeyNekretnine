using Application.Notifications.UserNotifications;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Handlers.EmailHandlers;
internal sealed class MultipleBanUserEmailHandler : INotificationHandler<MultipleBanUserNotification>
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;

    public MultipleBanUserEmailHandler(IServiceManager serviceManager, IRepositoryManager repositoryManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
    }
    public async Task Handle(MultipleBanUserNotification request, CancellationToken cancellationToken)
    {
        foreach (var userId in request.UserIds)
        {
            var userInformations = await _repositoryManager.User.GetEmailAndBanEndFromUserId(userId);

            var result = await _serviceManager.EmailService.SendUserBanEmail(userInformations.Item1, userInformations.Item2);

            if (!result)
            {
                throw new EmailCouldntBeSentException();
            }
        }

        await Task.CompletedTask;
    }
}
