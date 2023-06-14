using Shared.Error;

namespace Entities.DomainErrors
{
    public static partial class DomainErrors
    {
        public static class Agency
        {
            public static Error AgencyNotFound => new Error(
                "Agency.NotFound",
                "Agency not found");

            public static Error NotOwnerError => new Error(
                "Agency.NotOwner",
                "Current user is not owner of agency");

        }
    }

}
