namespace KeyNekretnine.Domain.ValueObjects;
public record AgentFirstName
{
    public const int MaxLength = 50;

    private AgentFirstName()
    {
    }

    public string? Value { get; init; }

    public static AgentFirstName? Create(string? agentFirstName)
    {
        if (string.IsNullOrWhiteSpace(agentFirstName))
        {
            throw new ApplicationException("Agent first name cannot be null or whitespace.");

        }

        if (agentFirstName.Length > MaxLength)
        {
            throw new ApplicationException($"Agent first name cannot exceed 50 characters.");

        }

        return new AgentFirstName
        {
            Value = agentFirstName
        };
    }
}