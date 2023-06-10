using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Email.Commands.SendConfirmEmail;
public sealed record SendConfirmEmailCommand(string Email) : ICommand<Unit>;

