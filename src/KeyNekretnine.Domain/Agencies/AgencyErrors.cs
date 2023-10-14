using KeyNekretnine.Domain.Abstraction;

namespace KeyNekretnine.Domain.Agencies;
public static class AgencyErrors
{
    public static Error NotFound => new(
            "Agency.NotFound",
            "Agency not found");

    public static Error NotOwner => new(
        "Agency.NotOwner",
        "Current user is not owner of agency");
}