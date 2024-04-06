using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
public sealed record UpdateAdvertLocationCommand(string ReferenceId, UpdateAdvertLocationRequest LocationUpdateRequest) : ICommand;