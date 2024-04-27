using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.PauseAdvert;
public sealed record PauseAdvertCommand(string ReferenceId) : ICommand;