using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ActivateAdvert;
public sealed record ActivateAdvertCommand(string ReferenceId, string UserId, bool IsAgency) : ICommand;