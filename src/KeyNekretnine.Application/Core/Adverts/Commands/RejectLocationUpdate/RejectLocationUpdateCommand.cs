using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectLocationUpdate;
public sealed record RejectLocationUpdateCommand(Guid UpdateId) : ICommand;