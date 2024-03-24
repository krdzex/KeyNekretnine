namespace KeyNekretnine.Domain.ValueObjects;
public record AdvertDescription
{
    public const int MaxLength = 1000;

    private AdvertDescription()
    {
    }

    public string Sr { get; init; }
    public string? En { get; init; }

    public static AdvertDescription Create(string serbianDescription, string englishDescription)
    {
        if (string.IsNullOrEmpty(serbianDescription))
        {
            throw new ApplicationException("Sr Description cannot be null or empty.");
        }

        if (serbianDescription.Length > MaxLength)
        {
            throw new ApplicationException("Sr Description cannot exceed 1000 characters.");
        }

        if (!string.IsNullOrEmpty(serbianDescription))
        {
            if (englishDescription.Length > MaxLength)
            {
                throw new ApplicationException("En Description cannot exceed 1000 characters.");
            }
        }

        return new AdvertDescription
        {
            Sr = serbianDescription,
            En = englishDescription
        };
    }
}