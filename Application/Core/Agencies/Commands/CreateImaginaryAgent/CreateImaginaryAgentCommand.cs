using Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Agency;

namespace Application.Core.Agencies.Commands.CreateImaginaryAgent;
public sealed record CreateImaginaryAgentCommand(ImaginaryAgentDto ImaginaryAgent, string Email, int AgencyId) : ICommand<Unit>;