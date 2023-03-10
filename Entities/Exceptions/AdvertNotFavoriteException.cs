namespace Entities.Exceptions;
public sealed class AdvertNotFavoriteException : BadRequestException
{
    public AdvertNotFavoriteException()
        : base("Advert is not favorite.")
    {
    }
}
