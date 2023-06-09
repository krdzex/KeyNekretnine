using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Users.Notifications.MultipleUserBan;
public sealed record MultipleUsersBanCommand(IEnumerable<string> Emails, int NoOfDays) : ICommand<Unit>;