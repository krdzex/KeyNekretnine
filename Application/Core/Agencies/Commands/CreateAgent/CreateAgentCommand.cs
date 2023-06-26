using Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Agency;

namespace Application.Core.Agencies.Commands.CreateAgent;
public sealed record CreateAgentCommand(CreateAgentDto Agent, string Email) : ICommand<Unit>;