using MediatR;

namespace Application.Notifications;
public sealed record DeclineAdvertNotification(int AdvertId) : INotification;

