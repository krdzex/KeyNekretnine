using Shared.Error;

namespace Entities.DomainErrors;
public static partial class DomainErrors
{
    public static class Advert
    {
        public static Error AdvertNotFound(int advertId) => new Error(
            "Advert.NotFound",
           $"Advert with id {advertId} doesn't exist in the database or it's not approved by admin.");

        public static Error AdminAdvertNotFound(int advertId) => new Error(
            "Advert.NotFound",
           $"Advert with id {advertId} doesn't exist in the database.");

        public static Error AdvertNotFavorite => new Error(
            "Advert.NotFavorite",
           $"Advert is not favorite");

        public static Error AdvertAlreadyFavorite => new Error(
            "Advert.AlreadyFavorite",
            $"Advert is already favorite");

        public static Error BadImagesCount => new Error(
            "Advert.ImageCountError",
           $"Advert is need at least 4 images");
    }
}