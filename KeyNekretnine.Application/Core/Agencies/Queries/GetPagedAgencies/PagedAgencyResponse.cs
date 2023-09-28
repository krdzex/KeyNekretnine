using KeyNekretnine.Application.Core.Shared;

namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
public sealed class PagedAgencyResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
    public string Email { get; init; }
    public string Image { get; init; }
    public string PhoneNumber { get; init; }
    public DateTime CreatedDate { get; init; }
    public int NumberOfAdverts { get; init; }
    public SocialMediaResponse SocialMedia { get; set; } = new();
}
