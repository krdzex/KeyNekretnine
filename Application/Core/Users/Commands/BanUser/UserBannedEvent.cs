using MediatR;

namespace Application.Core.Users.Notifications.BanUser
{
    public sealed record UserBannedEvent(string Email, int NoOfDays) : INotification;
}
