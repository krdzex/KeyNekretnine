using MediatR;

namespace Application.Commands.AdvertCommands;
public sealed record DeleteImagesCommand(IEnumerable<string> ImageUrls, int AdvertId) : IRequest;
