using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Commands.BanUser;
public sealed record BanUserCommand(string UserId, int NoOfDays) : ICommand;