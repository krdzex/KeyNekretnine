using KeyNekretnine.Application.Core.Agencies.Queries.GetById;

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
    public string TwitterUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string LinkedinUrl { get; set; }
    public List<LanguageResponse> Languages { get; set; } = new();
}
