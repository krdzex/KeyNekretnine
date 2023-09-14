using KeyNekretnine.Application.Abstraction.Messaging;
using Microsoft.AspNetCore.Http;

namespace KeyNekretnine.Application.Core.Agents.Commands.CreateAgent;
public sealed record CreateAgentCommand(
    Guid AgencyId,
    string UserId,
    string FirstName,
    string LastName,
    string Description,
    string Email,
    string PhoneNumber,
    string TwitterUrl,
    string FacebookUrl,
    string InstagramUrl,
    string LinkedinUrl,
    IFormFile Image,
    List<int> LanguageIds) : ICommand;