namespace Entities.Exceptions
{
    public sealed class AdvertNotFoundException : NotFoundException
    {
        public AdvertNotFoundException(int advertId)
            : base($"Advert with id {advertId} doesn't exist in the database or it's not approved by admin.")
        {
        }
    }
}
