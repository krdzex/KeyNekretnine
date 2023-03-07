using MediatR;

namespace Application.Notifications.UserNotifications;
public sealed record UnbanUserNotification(string UserId) : INotification;

