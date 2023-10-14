using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Agencies.Commands.CreateAgency;
public sealed record CreateAgencyCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    string AgencyName) : ICommand;