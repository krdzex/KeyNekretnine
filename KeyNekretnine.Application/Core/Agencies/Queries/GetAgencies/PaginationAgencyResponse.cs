using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
public sealed class PaginationAgencyResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
    public string Email { get; init; }
    public DateTime CreatedDate { get; init; }
    public int NumAdverts { get; init; }
    public SocialMediaResponse SocialMedia { get; set; } = new();
}
