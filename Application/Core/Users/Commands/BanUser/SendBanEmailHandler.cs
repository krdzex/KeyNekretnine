using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace KeyNekretnine.Application.Core.Users.Commands.BanUser;
internal sealed class SendBanEmailHandler : INotificationHandler<UserBannedEvent>
{
    private readonly IServiceManager _serviceManager;

    public SendBanEmailHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    public async Task Handle(UserBannedEvent request, CancellationToken cancellationToken)
    {
        var banEndDate = DateTime.Now.AddDays(request.NoOfDays);

        var result = await _serviceManager.EmailService.SendUserBanEmail(request.Email, banEndDate, cancellationToken);

        if (!result)
        {
            throw new EmailCouldntBeSentException();
        }

        await Task.CompletedTask;
    }
}