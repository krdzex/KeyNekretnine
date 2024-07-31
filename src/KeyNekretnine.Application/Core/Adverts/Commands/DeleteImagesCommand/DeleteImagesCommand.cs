using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.DeleteImagesCommand;
public sealed record DeleteImagesCommand(IEnumerable<string> ImageUrls, string ReferenceId) : ICommand;