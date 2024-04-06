using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveLocationUpdate;
public sealed record ApproveLocationUpdateCommand(Guid UpdateId) : ICommand;