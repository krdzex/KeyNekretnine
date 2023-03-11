using Application.Notifications.UserNotifications;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Handlers.EmailHandlers;
internal sealed class BanUserEmailHandler : INotificationHandler<BanUserNotification>
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;

    public BanUserEmailHandler(IServiceManager serviceManager, IRepositoryManager repositoryManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
    }
    public async Task Handle(BanUserNotification request, CancellationToken cancellationToken)
    {
        var userInformations = await _repositoryManager.User.GetEmailAndBanEndFromUserId(request.UserId, cancellationToken);

        var result = await _serviceManager.EmailService.SendUserBanEmail(userInformations.Item1, userInformations.Item2);

        if (!result)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}