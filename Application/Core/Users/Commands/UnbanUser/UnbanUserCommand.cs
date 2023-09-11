using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Commands.UnbanUser;
public sealed record UnbanUserCommand(string UserId) : ICommand;