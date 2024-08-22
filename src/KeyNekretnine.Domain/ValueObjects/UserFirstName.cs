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
        if (!string.IsNullOrEmpty(firstName))
        {
            if (firstName.Length > MaxLength)
            {
                throw new ApplicationException($"First name cannot exceed 50 characters.");

            }
        }

        return new UserFirstName
        {
            Value = firstName
        };
    }
}