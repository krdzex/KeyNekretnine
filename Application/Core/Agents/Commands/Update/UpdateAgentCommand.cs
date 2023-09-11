using KeyNekretnine.Application.Abstraction.Messaging;
using Microsoft.AspNetCore.Http;

namespace KeyNekretnine.Application.Core.Agents.Commands.Update;
public sealed record UpdateAgentCommand(
    Guid AgentId,
    string UserId,
    string FirstName,
    string LastName,
    string Description,
    string Email,
    string TwitterUrl,
    string FacebookUrl,
    string InstagramUrl,
    string LinkedinUrl,
    string PhoneNumber,
    IFormFile Image,
    List<int> LanguageIds) : ICommand;