using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Commands.PasswordForgot;
public sealed record PasswordForgotCommand(string Email, string Token, string NewPassword) : ICommand;