using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
public sealed class PaginationAgencyResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; }
    public int NumAdverts { get; set; }
    public SocialNetworkResponse SocialNetwork { get; set; }
}
