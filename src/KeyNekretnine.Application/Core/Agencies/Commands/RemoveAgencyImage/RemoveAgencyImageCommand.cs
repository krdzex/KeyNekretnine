using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Commands.RemoveAgenyImage;
public sealed record RemoveAgencyImageCommand(Guid AgencyId) : ICommand;