using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveFeaturesUpdate;
public sealed record ApproveFeaturesUpdateCommand(Guid UpdateId) : ICommand;
