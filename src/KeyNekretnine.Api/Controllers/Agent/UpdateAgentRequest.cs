namespace KeyNekretnine.Api.Controllers.Agent;

public sealed record UpdateAgentRequest(
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
    List<int> LanguageIds);
