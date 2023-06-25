using Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Agency;

namespace Application.Core.Agencies.Commands.UpdateAgency;
public sealed record UpdateAgencyCommand(UpdateAgencyDto UpdateAgency, int AgencyId, string Email) : ICommand<Unit>;