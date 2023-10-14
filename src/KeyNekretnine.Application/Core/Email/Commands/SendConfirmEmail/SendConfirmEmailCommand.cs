using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;

namespace KeyNekretnine.Application.Core.Email.Commands.SendConfirmEmail;
public sealed record SendConfirmEmailCommand(string Email) : ICommand<Unit>;