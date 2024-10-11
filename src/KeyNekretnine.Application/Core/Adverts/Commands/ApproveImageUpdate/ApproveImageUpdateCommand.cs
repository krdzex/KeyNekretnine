using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveImageUpdate;
public sealed record ApproveImageUpdateCommand(Guid UpdateId) : ICommand;