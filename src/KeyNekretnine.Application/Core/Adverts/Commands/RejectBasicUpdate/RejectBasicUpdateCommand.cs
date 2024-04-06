using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectBasicUpdate;
public sealed record RejectBasicUpdateCommand(Guid UpdateId) : ICommand;