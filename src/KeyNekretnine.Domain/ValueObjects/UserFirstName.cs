namespace KeyNekretnine.Domain.ValueObjects;
public record UserFirstName
{
    public const int MaxLength = 50;

    private UserFirstName()
    {
    }

    public string? Value { get; init; }

    public static UserFirstName? Create(string? firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ApplicationException("First name cannot be null or whitespace.");

        }

        if (firstName.Length > MaxLength)
        {
            throw new ApplicationException($"First name cannot exceed 50 characters.");

        }

        return new UserFirstName
        {
            Value = firstName
        };
    }
}