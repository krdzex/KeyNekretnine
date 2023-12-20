namespace KeyNekretnine.Domain.ValueObjects;
public record AgencyName
{
    public const int MaxLength = 50;

    private AgencyName()
    {
    }

    public string Value { get; init; }

    public static AgencyName Create(string agencyName)
    {
        if (string.IsNullOrEmpty(agencyName))
        {
            throw new ApplicationException("Agency name cannot be null or empty.");
        }

        if (agencyName.Length > MaxLength)
        {
            throw new ApplicationException("Agency name cannot exceed 50 characters.");
        }

        return new AgencyName
        {
            Value = agencyName
        };
    }
}