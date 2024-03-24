using System.Security.Claims;

namespace KeyNekretnine.Infrastructure.Authentication;
internal static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
               throw new ApplicationException("User identity is unavailable");
    }

    public static bool GetIsAgency(this ClaimsPrincipal? principal)
    {
        var result = principal?.FindFirstValue("isAgency") ??
               throw new ApplicationException("User identity is unavailable");

        return bool.Parse(result);
    }
}
