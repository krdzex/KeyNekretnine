using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyLocation;
using KeyNekretnine.Application.Core.Language.Queries.GetLanguages;
using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyById;
public sealed class AgencyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Image { get; set; }
    public string WebsiteUrl { get; set; }
    public TimeSpan? WorkStartTime { get; set; }
    public TimeSpan? WorkEndTime { get; set; }
    public List<LanguageResponse> Languages { get; set; } = new();
    public SocialMediaResponse SocialMedia { get; set; } = new();
    public AgencyLocationResponse Location { get; set; } = new();
}
