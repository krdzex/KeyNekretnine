namespace KeyNekretnine.Domain.ValueObjects;
public record SocialMedia
{
    public const int MaxLength = 300;
    private SocialMedia()
    {
    }

    public string? Twitter { get; init; }
    public string? Facebook { get; init; }
    public string? Instagram { get; init; }
    public string? Linkedin { get; init; }
    public static SocialMedia Create(string? twitter, string? facebook, string? instagram, string? linkedin)
    {
        if (!string.IsNullOrEmpty(twitter))
        {
            if (twitter.Length > MaxLength)
            {
                throw new ApplicationException($"Twitter url cannot exceed 300 characters.");
            }
        }
        else
        {
            twitter = null;
        }

        if (!string.IsNullOrEmpty(facebook))
        {
            if (facebook.Length > MaxLength)
            {
                throw new ApplicationException($"Facebook cannot exceed 50 characters.");
            }
        }
        else
        {
            facebook = null;
        }

        if (!string.IsNullOrEmpty(instagram))
        {
            if (instagram.Length > MaxLength)
            {
                throw new ApplicationException($"Instagram cannot exceed 50 characters.");
            }
        }
        else
        {
            instagram = null;
        }

        if (!string.IsNullOrEmpty(linkedin))
        {
            if (linkedin.Length > MaxLength)
            {
                throw new ApplicationException($"Linkedin cannot exceed 50 characters.");
            }
        }
        else
        {
            linkedin = null;
        }

        return new SocialMedia
        {
            Twitter = twitter,
            Facebook = facebook,
            Instagram = instagram,
            Linkedin = linkedin
        };
    }
}