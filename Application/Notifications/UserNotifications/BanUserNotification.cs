using MediatR;

namespace Application.Notifications.UserNotifications;
public sealed record BanUserNotification(string UserId, int NoOfDays) : INotification;