using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Agency;

namespace KeyNekretnine.Application.Core.Agencies.Commands.UpdateAgency;
public sealed record UpdateAgencyCommand(UpdateAgencyDto UpdateAgency, int AgencyId, string Email) : ICommand<Unit>;