using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.RequestFeatures;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service;
internal sealed class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> CreateToken(User user)
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
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
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

    public async Task<TokenRequest> VerifyRefreshToken(TokenRequest request)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
        var email = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == ClaimTypes.Email)?.Value;
        var user = await _userManager.FindByEmailAsync(email);

        var isValid = await _userManager.VerifyUserTokenAsync(user, "KeyNekretnineAPI", "RefreshToken", request.RefreshToken);
        if (!isValid)
        {
            await _userManager.UpdateSecurityStampAsync(user);
            return null;
        }
        return new TokenRequest { Token = await CreateToken(user), RefreshToken = await CreateRefreshToken(user) };
    }

    public string ValidateJwtToken(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken))
            return null;

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler().ValidateToken(jwtToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))),
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var jwtClaims = (JwtSecurityToken)validatedToken;
            var email = jwtClaims.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;

            return email;
        }
        catch
        {
            return null;
        }
    }
}