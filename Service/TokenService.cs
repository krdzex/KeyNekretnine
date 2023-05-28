﻿using Entities.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Service.Contracts;
using Shared.DataTransferObjects.Auth;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service;
internal sealed class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public TokenService(UserManager<User> userManager, IConfiguration configuration, HttpClient httpClient)
    {
        _userManager = userManager;
        _configuration = configuration;
        _httpClient = httpClient;
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

    public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleLoginDto googleLoginDto)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.IdToken, settings);
            return payload;
        }
        catch
        {
            //log an exception
            throw new UnauthorizedAccessException("Invalid token");
        }
    }

    public async Task<FBUserInfoDto> VerifyFacebookTokenAndGetUserInfo(string fbAccessToken)
    {
        try
        {
            var fbAppId = Environment.GetEnvironmentVariable("FACEBOOK_APP_ID");
            var fbSecret = Environment.GetEnvironmentVariable("FACEBOOK_SECRET");

            HttpResponseMessage debugTokenResponse = await _httpClient.GetAsync("https://graph.facebook.com/debug_token?input_token=" + fbAccessToken + $"&access_token={fbAppId}|{fbSecret}");

            var stringThing = await debugTokenResponse.Content.ReadAsStringAsync();
            var userOBJK = JsonConvert.DeserializeObject<FBUserDto>(stringThing);

            if (userOBJK.Data.IsValid == false)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            HttpResponseMessage meResponse = await _httpClient.GetAsync("https://graph.facebook.com/me?fields=first_name,last_name,email,id&access_token=" + fbAccessToken);
            var userContent = await meResponse.Content.ReadAsStringAsync();
            var userContentObj = JsonConvert.DeserializeObject<FBUserInfoDto>(userContent);

            return userContentObj;
        }
        catch
        {
            throw new UnauthorizedAccessException("Invalid token");
        }
    }
}