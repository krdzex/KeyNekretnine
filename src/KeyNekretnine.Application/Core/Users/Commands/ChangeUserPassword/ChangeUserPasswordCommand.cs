using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Commands.ChangeUserPassword;
public sealed record ChangeUserPasswordCommand(string CurrentPassword, string NewPassword) : ICommand;