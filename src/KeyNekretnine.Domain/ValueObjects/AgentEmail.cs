namespace KeyNekretnine.Domain.ValueObjects;
public record AgentEmail
{
    public const int MaxLength = 100;

    private AgentEmail()
    {
    }

    public string? Value { get; init; }

    public static AgentEmail? Create(string? agentEmail)
    {
        if (string.IsNullOrEmpty(agentEmail))
        {
            throw new ApplicationException("Agent email cannot be null or empty.");
        }

        if (agentEmail.Length > MaxLength)
        {
            throw new ApplicationException($"Email cannot exceed 100 characters.");
        }

        return new AgentEmail
        {
            Value = agentEmail
        };
    }
}