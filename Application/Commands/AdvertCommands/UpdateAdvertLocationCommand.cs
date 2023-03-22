using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Commands.AdvertCommands;
public sealed record UpdateAdvertLocationCommand(UpdateAdvertLocationDto UpdateAdvertLocationDto, int AdvertId) : IRequest;
