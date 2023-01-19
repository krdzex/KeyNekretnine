using MediatR;

namespace Application.Commands.UserCommands;
public sealed record BanUserCommand(string UserId, int NoOfDays) : IRequest;

