namespace KeyNekretnine.Api.Controllers.Agent;

public sealed record CreateAgentRequest(
    Guid AgencyId,
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
    List<int> LanguageIds);
