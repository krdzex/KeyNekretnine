using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Adverts;
public static class AdvertErrors
{
    public static Error NotFound => new(
            "Advert.NotFound",
            "Advert doesn't exist in the database or it's not approved by admin.");

    public static Error NotFoundForAdmin => new(
        "Advert.NotFound",
        "Advert doesn't exist in the database.");

    public static Error AlreadyAccepted => new(
        "Advert.AlreadyAccepted",
        "Advert is already accepted");
}