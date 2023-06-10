using Shared.Error;

namespace Entities.DomainErrors
{
    public static partial class DomainErrors
    {
        public static class Token
        {
            public static Error BadTokens => new Error(
                   "Token.CantVarifyTokens",
                   "Cant varify tokens.");
        }
    }
}
