namespace Entities.Exceptions;
public sealed class AdvertAlreadyFavoriteException : BadRequestException
{
    public AdvertAlreadyFavoriteException()
        : base("Advert already favorite.")
    {
    }
}
