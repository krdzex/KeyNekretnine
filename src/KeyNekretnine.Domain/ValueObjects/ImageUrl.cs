using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.ValueObjects;
public sealed class ImageUrl : ValueObject
{
    public const int MaxLength = 200;
    private ImageUrl(string value)
    {
        Value = value;
    }

    public string Value { get; }
    public static ImageUrl? Create(string? imageUrl)
    {
        if (imageUrl is null)
        {
            return null;
        }

        if (imageUrl.Length > MaxLength)
        {
            throw new ApplicationException($"Image url cannot exceed 200 characters.");
        }

        return new ImageUrl(imageUrl);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}