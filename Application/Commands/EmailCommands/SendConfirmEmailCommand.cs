using MediatR;

namespace Application.Commands.EmailCommands;
public sealed record SendConfirmEmailCommand(string Email) : IRequest;

