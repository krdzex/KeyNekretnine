using Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Agency;

namespace Application.Core.Agents.Commands.UpdateAgent;
public sealed record UpdateAgentCommand(UpdateAgentDto Agent, int AgentId, string Email) : ICommand<Unit>;
