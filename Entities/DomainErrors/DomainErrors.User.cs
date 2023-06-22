using Shared.Error;

namespace Entities.DomainErrors;
public static partial class DomainErrors
{
    public static class User
    {
        public static Error UserNotFound => new Error(
            "User.NotFound",
            "User doesnt exist in database");
    }
}