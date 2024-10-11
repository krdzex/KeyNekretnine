using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectImageUpdate;
public sealed record RejectImageUpdateCommand(Guid UpdateId) : ICommand;