using KeyNekretnine.Application.Abstraction.Messaging;
using Microsoft.AspNetCore.Http;

namespace KeyNekretnine.Application.Core.Agents.Commands.UpdateAgent;
public sealed record UpdateAgentCommand(
    Guid AgentId,
    string UserId,
    string FirstName,
    string LastName,
    string Description,
    string Email,
    string Twitter,
    string Facebook,
    string Instagram,
    string Linkedin,
    string PhoneNumber,
    IFormFile Image,
    List<int> LanguageIds) : ICommand;