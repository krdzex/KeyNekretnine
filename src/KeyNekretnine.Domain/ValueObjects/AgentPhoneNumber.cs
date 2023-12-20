namespace KeyNekretnine.Domain.ValueObjects;
public record AgentPhoneNumber
{
    public const int MaxLength = 50;

    private AgentPhoneNumber()
    {
    }

    public string? Value { get; init; }

    public static AgentPhoneNumber? Create(string? agentPhoneNumber)
    {
        if (string.IsNullOrEmpty(agentPhoneNumber))
        {
            throw new ApplicationException("Agent phone number cannot be null or whitespace.");
        }

        if (agentPhoneNumber.Length > MaxLength)
        {
            throw new ApplicationException($"Agent phone number cannot exceed 50 characters.");
        }

        return new AgentPhoneNumber
        {
            Value = agentPhoneNumber
        };
    }
}