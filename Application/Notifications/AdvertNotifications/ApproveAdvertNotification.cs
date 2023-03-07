using MediatR;

namespace Application.Notifications.AdvertNotifications;
public sealed record ApproveAdvertNotification(int AdvertId) : INotification;
