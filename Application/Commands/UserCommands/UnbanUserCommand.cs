using MediatR;

namespace Application.Commands.UserCommands;
public sealed record UnbanUserCommand(string UserId) : IRequest;

