using MediatR;

namespace Application.Core.Users.Commands.MultipleUsersUnban;
public sealed record UsersUnbannedEvent(IEnumerable<string> Emails) : INotification;