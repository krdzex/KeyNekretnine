using MediatR;

namespace KeyNekretnine.Application.Core.Users.Commands.MultipleUsersBan;
public sealed record UsersBannedEvent(IEnumerable<string> Emails, int NoOfDays) : INotification;