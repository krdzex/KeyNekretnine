using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ChangeAgentForAdvert;
public sealed record ChangeAgentForAdvertCommand(string ReferenceId, Guid NewAgentId) : ICommand;