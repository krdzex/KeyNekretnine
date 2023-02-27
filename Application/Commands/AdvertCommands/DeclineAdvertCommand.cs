using MediatR;

namespace Application.Commands.AdvertCommands;
public sealed record DeclineAdvertCommand(int AdvertId) : IRequest;
