using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Users.Commands.MultipleUsersBan;
public sealed record MultipleUsersBanCommand(IEnumerable<string> Emails, int NoOfDays) : ICommand<Unit>;