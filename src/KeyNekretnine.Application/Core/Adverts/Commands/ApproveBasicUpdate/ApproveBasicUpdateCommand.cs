using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveBasicUpdate;
public sealed record ApproveBasicUpdateCommand(Guid UpdateId) : ICommand;