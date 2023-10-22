using KeyNekretnine.Application.Core.Language.Queries.GetLanguages;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
public sealed class AgentResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public SocialMediaResponse SocialMedia { get; set; } = new();
    public List<LanguageResponse> Languages { get; set; } = new();
    public ShortAgencyResponse Agency { get; set; }
}
