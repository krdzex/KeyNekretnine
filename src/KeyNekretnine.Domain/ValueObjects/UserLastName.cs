namespace KeyNekretnine.Domain.ValueObjects;
public record UserLastName
{
    public const int MaxLength = 50;

    private UserLastName()
    {
    }

    public string Value { get; init; }

    public static UserLastName Create(string lastName)
    {
        if (!string.IsNullOrEmpty(lastName))
        {
            if (lastName.Length > MaxLength)
            {
                throw new ApplicationException($"Last name cannot exceed characters.");

            }
        }

        return new UserLastName
        {
            Value = lastName
        };
    }
}