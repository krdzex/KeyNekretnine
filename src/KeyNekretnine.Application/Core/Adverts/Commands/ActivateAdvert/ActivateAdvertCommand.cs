using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ActivateAdvert;
public sealed record ActivateAdvertCommand(string ReferenceId) : ICommand;