using KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Agents.Queries.GetPagedAgents;
public sealed class PagedAgentResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public int NumberOfAdverts { get; set; }
    public ShortAgencyResponse Agency { get; set; }
    public SocialMediaResponse SocialMedia { get; set; } = new();
}
