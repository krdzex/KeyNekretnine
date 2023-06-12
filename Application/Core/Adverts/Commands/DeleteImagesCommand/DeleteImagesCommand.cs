using Application.Abstraction.Messaging;
using MediatR;

namespace Application.Core.Adverts.Commands.DeleteImagesCommand;
public sealed record DeleteImagesCommand(IEnumerable<string> ImageUrls, int AdvertId) : ICommand<Unit>;
