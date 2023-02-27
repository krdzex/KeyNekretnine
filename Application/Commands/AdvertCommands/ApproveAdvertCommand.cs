using MediatR;

namespace Application.Commands.AdvertCommands;
public sealed record ApproveAdvertCommand(int AdvertId) : IRequest;

