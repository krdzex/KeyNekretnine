using Entities.Exceptions;
using MediatR;
using Service.Contracts;

namespace Application.Core.Users.Notifications.MultipleUserBan
{
    internal sealed class SendMultipleBanEmailHandler : INotificationHandler<UsersBannedEvent>
    {
        private readonly IServiceManager _serviceManager;

        public SendMultipleBanEmailHandler(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        public async Task Handle(UsersBannedEvent request, CancellationToken cancellationToken)
        {
            foreach (var email in request.Emails)
            {
                var banEndDate = DateTime.Now.AddDays(request.NoOfDays);

                var result = await _serviceManager.EmailService.SendUserBanEmail(email, banEndDate, cancellationToken);

                if (!result)
                {
                    throw new EmailCouldntBeSentException();
                }
            }

            await Task.CompletedTask;
        }
    }
}
