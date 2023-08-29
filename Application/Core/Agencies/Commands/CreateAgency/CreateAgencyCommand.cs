using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Agency;

namespace KeyNekretnine.Application.Core.Agencies.Commands.CreateAgency;
public sealed record CreateAgencyCommand(CreateAgencyDto CreateAgencyDto) : ICommand<Unit>;