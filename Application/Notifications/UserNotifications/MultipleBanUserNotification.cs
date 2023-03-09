using MediatR;

namespace Application.Notifications.UserNotifications;
public sealed record MultipleBanUserNotification(IEnumerable<string> UserIds, int NoOfDays) : INotification;
