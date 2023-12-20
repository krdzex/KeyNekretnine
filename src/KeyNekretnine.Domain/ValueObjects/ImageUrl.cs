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
    public static Result<ImageUrl> Create(string? imageUrl)
    {
        if (imageUrl is null)
        {
            return null;
        }

        if (imageUrl.Length > MaxLength)
        {
            return Result.Failure<ImageUrl>(new Error("", ""));
        }

        return new ImageUrl(imageUrl);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}