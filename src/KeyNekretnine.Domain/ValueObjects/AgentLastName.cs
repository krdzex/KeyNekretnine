namespace KeyNekretnine.Domain.ValueObjects;
public record AgentLastName
{
    public const int MaxLength = 50;

    private AgentLastName()
    {
    }

    public string? Value { get; init; }

    public static AgentLastName Create(string? agentLastName)
    {
        if (string.IsNullOrWhiteSpace(agentLastName))
        {
            throw new ApplicationException("Agent last name cannot be null or whitespace.");
        }

        if (agentLastName.Length > MaxLength)
        {
            throw new ApplicationException($"Agent last name cannot exceed characters.");

        }

        return new AgentLastName
        {
            Value = agentLastName
        };
    }
}