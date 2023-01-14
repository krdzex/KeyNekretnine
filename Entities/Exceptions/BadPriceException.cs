namespace Entities.Exceptions;
public sealed class BadPriceException : BadRequestException
{
    public BadPriceException()
        : base("Max price can't be less than min price.")
    {
    }
}