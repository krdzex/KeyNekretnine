using MediatR;

namespace Application.Notifications;
public sealed record ApproveAdvertNotification(int AdvertId) : INotification;
