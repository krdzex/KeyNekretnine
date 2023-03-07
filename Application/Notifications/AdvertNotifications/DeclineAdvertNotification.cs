using MediatR;

namespace Application.Notifications.AdvertNotifications;
public sealed record DeclineAdvertNotification(int AdvertId) : INotification;

