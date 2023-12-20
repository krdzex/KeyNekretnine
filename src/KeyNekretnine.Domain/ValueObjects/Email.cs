namespace KeyNekretnine.Domain.Shared;
public record Email
{
    public const int MaxLength = 100;

    private Email()
    {
    }

    public string? Value { get; init; }

    public static Email? Create(string? email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return null;
        }

        if (email.Length > MaxLength)
        {
            throw new ApplicationException($"Email cannot exceed 100 characters.");
        }

        return new Email
        {
            Value = email
        };
    }
}