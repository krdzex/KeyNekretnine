using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Agencies;
public static class AgencyErrors
{
    public static Error AgencyNotFound => new Error(
            "Agency.NotFound",
            "Agency not found");

    public static Error NotOwnerError => new Error(
        "Agency.NotOwner",
        "Current user is not owner of agency");
}