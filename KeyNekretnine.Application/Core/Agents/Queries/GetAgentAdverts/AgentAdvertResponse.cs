namespace KeyNekretnine.Application.Core.Agents.Queries.GetAgentAdverts;
public sealed class AgentAdvertResponse
{
    public string Location { get; set; }
    public double Price { get; set; }
    public string CoverImageUrl { get; set; }
    public int NoOfBathrooms { get; set; }
    public int NoOfBedrooms { get; set; }
    public double FloorSpace { get; set; }
    public string Street { get; set; }
}
