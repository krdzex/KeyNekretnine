namespace KeyNekretnine.Domain.ValueObjects;
public record WebsiteUrl
{
    public const int MaxLength = 200;
    private WebsiteUrl()
    {
    }

    public string? Value { get; init; }
    public static WebsiteUrl? Create(string? websiteUrl)
    {
        if (string.IsNullOrEmpty(websiteUrl))
        {
            return null;
        }

        if (websiteUrl.Length > MaxLength)
        {
            throw new ApplicationException($"Website url cannot exceed 200 characters.");
        }

        return new WebsiteUrl
        {
            Value = websiteUrl
        };
    }
}