using KeyNekretnine.Application.Core.Language.Queries.Get;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyById;
public sealed class AgencyResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string WebsiteUrl { get; set; }
    public TimeSpan? WorkStartTime { get; set; }
    public TimeSpan? WorkEndTime { get; set; }
    public List<LanguageResponse> Languages { get; set; } = new();
    public SocialNetworkResponse SocialNetwork { get; set; }
}
