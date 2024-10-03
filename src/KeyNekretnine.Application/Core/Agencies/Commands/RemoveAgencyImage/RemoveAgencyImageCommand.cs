using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Commands.RemoveAgencyImage;
public sealed record RemoveAgencyImageCommand(Guid AgencyId) : ICommand;