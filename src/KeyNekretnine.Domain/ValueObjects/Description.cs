namespace KeyNekretnine.Domain.ValueObjects;
public record Description
{
    public const int MaxLength = 1000;

    private Description()
    {
    }

    public string? Value { get; init; }

    public static Description? Create(string? description)
    {
        if (string.IsNullOrEmpty(description))
        {
            return null;
        }

        if (description.Length > MaxLength)
        {
            throw new ApplicationException($"Description cannot exceed 1000 characters.");
        }

        return new Description
        {
            Value = description
        };
    }
}