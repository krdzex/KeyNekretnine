using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Commands.ConfirmUserEmail;
public sealed record ConfirmUserEmailCommand(string Token, string Email) : ICommand<bool>;