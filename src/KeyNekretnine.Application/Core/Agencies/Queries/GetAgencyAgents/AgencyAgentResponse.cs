namespace KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAgents;
public sealed class AgencyAgentResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Image { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int numberOfAdverts { get; set; }
}
