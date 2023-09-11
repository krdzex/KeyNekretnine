using KeyNekretnine.Application.Core.Language.Queries.Get;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetById;
public sealed class AgencyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string WebsiteUrl { get; set; }
    public TimeSpan? WorkStartTime { get; set; }
    public TimeSpan? WorkEndTime { get; set; }
    public List<LanguageResponse> Languages { get; set; } = new();
    public SocialMediaResponse SocialMedia { get; set; }
}
