using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectFeaturesUpdate;
public sealed record RejectFeaturesUpdateCommand(Guid UpdateId) : ICommand;