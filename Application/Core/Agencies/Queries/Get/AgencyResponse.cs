namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
public sealed class AgencyResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string TwitterUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string LinkedinUrl { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public int NumAdverts { get; set; }
}
