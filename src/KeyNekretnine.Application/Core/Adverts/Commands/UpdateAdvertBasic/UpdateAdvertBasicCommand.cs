using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertBasic;
public sealed record UpdateAdvertBasicCommand(string ReferenceId, UpdateAdvertBasicRequest BasicUpdateData) : ICommand;
