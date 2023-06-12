using Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Agency;

namespace Application.Core.Agencies.Commands.CreateAgency;
public sealed record CreateAgencyCommand(CreateAgencyDto CreateAgencyDto) : ICommand<Unit>;
