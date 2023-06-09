using MediatR;

namespace Application.Core.Users.Notifications.MultipleUserBan;
public sealed record UsersBannedEvent(IEnumerable<string> Emails, int NoOfDays) : INotification;
