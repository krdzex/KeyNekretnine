using KeyNekretnine.Application.Abstraction.Authentication;
using Microsoft.AspNetCore.Http;

namespace KeyNekretnine.Infrastructure.Authentication;
internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

    public bool IsAgency =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetIsAgency() ??
        throw new ApplicationException("User context is unavailable");
}
