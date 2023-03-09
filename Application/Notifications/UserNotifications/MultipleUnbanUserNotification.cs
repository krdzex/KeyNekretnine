using MediatR;

namespace Application.Notifications.UserNotifications;
public sealed record MultipleUnbanUserNotification(IEnumerable<string> UserIds) : INotification;

