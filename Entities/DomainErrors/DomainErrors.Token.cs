using Shared.Error;

namespace Entities.DomainErrors
{
    public static partial class DomainErrors
    {
        public static class Token
        {
            public static Error BadToken => new Error(
                   "Token.CantVarifyToken",
                   "Cant varify token.");
        }
    }
}
