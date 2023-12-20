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

    public static Error AlreadyRejected => new(
        "Advert.AlreadyRejected",
        "Advert is already rejected");

    public static Error NotOwner => new(
        "Advert.NotOwner",
        "Current user is not owner of advert");

    public static Error AlreadyPremium => new(
        "Advert.AlreadyPremium",
        "Advert is already premium");

    public static Error NotPremium => new(
        "Advert.NotPremium",
        "Advert is not premium");
}