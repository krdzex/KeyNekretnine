using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ChangeCoverImage;
public sealed record ChangeCoverImageCommand(string ReferenceId, string NewCoverUrl) : ICommand;