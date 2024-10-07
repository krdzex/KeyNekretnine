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

    public static Error AlreadyPaused => new(
        "Advert.AlreadyPaused",
        "Advert is already paused");

    public static Error NotPaused => new(
        "Advert.NotPaused",
        "Advert is not paused");

    public static Error BasicUpdateAlredyExist => new(
        "Advert.BasicUpdateAlredyExist",
        "Basic update already exist, wait untill admin approve or reject your update");

    public static Error ImageUpdateAlredyExist => new(
        "Advert.ImageUpdateAlredyExist",
        "Image update already exist, wait untill admin approve or reject your update");

    public static Error LocationcUpdateAlredyExist => new(
        "Advert.LocationcUpdateAlredyExist",
        "Location update already exist, wait untill admin approve or reject your update");

    public static Error BasicUpdateNotFound => new(
        "Advert.BasicUpdateNotFound",
        "Basic update with this id doesnt exist");

    public static Error LocationUpdateNotFound => new(
        "Advert.LocationUpdateNotFound",
        "Location update with this id doesnt exist");

    public static Error FeaturesUpdateNotFound => new(
        "Advert.FeaturesUpdateNotFound",
        "Features update with this id doesnt exist");

    public static Error BasicUpdateApproved => new(
        "Advert.BasicUpdateApproved",
        "Bacis update is approved already");

    public static Error FeaturesUpdateApproved => new(
        "Advert.FeaturesUpdateApproved",
        "Features update is approved already");

    public static Error LocationUpdateApproved => new(
        "Advert.LocationUpdateApproved",
        "Location update is approved");

    public static Error BasicUpdateRejected => new(
        "Advert.BasicUpdateRejected",
        "Bacis update is rejected");

    public static Error FeaturesUpdateRejected => new(
        "Advert.FeaturesUpdateRejected",
        "Features update is rejected");

    public static Error LocationUpdateRejected => new(
        "Advert.LocationUpdateRejected",
        "Location update is rejected");

    public static Error NoEnoughImages => new(
        "Advert.NoEnoughImages",
        "You need to have at least 2 images");

    public static Error ImageNotFound => new(
        "Advert.ImageNotFound",
        "Image not found");
}