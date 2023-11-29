using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Core.Auth.Commands.RefreshTokens;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KeyNekretnine.Infrastructure.Authentication;
internal sealed class JwtService : IJwtService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public JwtService(
        UserManager<User> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> CreateAccessToken(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var token = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var token = new JwtSecurityToken(
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration.GetSection("Jwt").GetSection("Lifetime").Value)),
            signingCredentials: signingCredentials
            );

        return token;
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("isAgency", user.IsAgency.ToString())
            };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Environment.GetEnvironmentVariable("JWT_KEY");
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<string> CreateRefreshToken(User user)
    {
        await _userManager.RemoveAuthenticationTokenAsync(user, "KeyNekretnineAPI", "RefreshToken");
        var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, "KeyNekretnineAPI", "RefreshToken");
        await _userManager.SetAuthenticationTokenAsync(user, "KeyNekretnineAPI", "RefreshToken", newRefreshToken);
        return newRefreshToken;
    }

    public async Task<RefreshTokenResponse> VerifyRefreshToken(string accessToken, string refreshToken)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(accessToken);
        var userId = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId);

        var isValid = await _userManager.VerifyUserTokenAsync(
            user,
            "KeyNekretnineAPI",
            "RefreshToken",
            refreshToken);

        if (!isValid)
        {
            await _userManager.UpdateSecurityStampAsync(user);
            return null;
        }

        var newAccessToken = await CreateAccessToken(user);
        var newRefreshToken = await CreateRefreshToken(user);

        return new RefreshTokenResponse { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
    }
}
