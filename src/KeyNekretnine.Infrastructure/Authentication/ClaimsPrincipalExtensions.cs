using System.Security.Claims;

namespace KeyNekretnine.Infrastructure.Authentication;
internal static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
               throw new ApplicationException("User identity is unavailable");
    }

    public static Guid? GetAgencyId(this ClaimsPrincipal? principal)
    {
        var agencyIdStrig = principal?.FindFirstValue("AgencyId") ??
               throw new ApplicationException("User identity is unavailable");

        if (string.IsNullOrWhiteSpace(agencyIdStrig))
        {
            return null;
        }

        return new Guid(agencyIdStrig);
    }
}
