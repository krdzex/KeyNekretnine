namespace KeyNekretnine.Domain.ValueObjects;
public record PhoneNumber
{
    public const int MaxLength = 50;

    private PhoneNumber()
    {
    }

    public string? Value { get; init; }

    public static PhoneNumber? Create(string? phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return null;
        }

        if (phoneNumber.Length > MaxLength)
        {
            throw new ApplicationException($"Phone number cannot exceed 50 characters.");
        }

        return new PhoneNumber
        {
            Value = phoneNumber
        };
    }
}