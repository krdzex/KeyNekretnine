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
    string Twitter,
    string Facebook,
    string Instagram,
    string Linkedin,
    IFormFile Image,
    List<int> LanguageIds) : ICommand;