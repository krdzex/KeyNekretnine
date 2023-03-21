using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Commands.AdvertCommands;
public sealed record UpdateAdvertCommand(UpdateAdvertInformationsDto updateAdvertInformationsDto, int AdvertId) : IRequest;
