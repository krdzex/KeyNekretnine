using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Commands.RequestPasswordForgot;
public sealed record RequestPasswordForgotCommand(string Email) : ICommand;