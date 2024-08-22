namespace KeyNekretnine.Domain.ValueObjects;
public record About
{
    public const int MaxLength = 1000;

    private About()
    {
    }

    public string Value { get; init; }


    public static About Create(string about)
    {
        if (!string.IsNullOrEmpty(about))
        {
            if (about.Length > MaxLength)
            {
                throw new ApplicationException("User about cannot exceed 1000 characters.");
            }
        }

        return new About
        {
            Value = about
        };
    }
}