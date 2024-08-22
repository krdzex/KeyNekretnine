using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agents.Commands.RemoveAgentImage;
public sealed record RemoveAgentImageCommand(Guid AgentId) : ICommand;