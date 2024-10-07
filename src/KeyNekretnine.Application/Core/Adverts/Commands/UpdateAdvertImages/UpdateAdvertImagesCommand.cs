using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertImages;
public sealed record UpdateAdvertImagesCommand(string ReferenceId, UpdateAdvertImagesRequest ImageUpdateData) : ICommand;
