using KeyNekretnine.Application.Core.Auth.Commands.GoogleLogin;
using KeyNekretnine.Application.Core.Auth.Commands.RefreshTokens;
using KeyNekretnine.Application.Core.Auth.Commands.UserLogin;
using KeyNekretnine.Application.Core.Auth.Commands.UserRegistration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Api.Controllers.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;
    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        var command = new UserRegistrationCommand(
            registerUserRequest.Email,
            registerUserRequest.Password);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? StatusCode(201) : BadRequest(response.Error);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LogInUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        var response = await _sender.Send(command, cancellationToken);

        if (response.IsFailure)
        {
            return Unauthorized(response.Error);
        }

        HttpContext.Response.Cookies.Append("X-Access-Token", response.Value.AccessToken,
        new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7),
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        });

        HttpContext.Response.Cookies.Append("X-Refresh-Token", response.Value.RefreshToken,
        new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7),
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        });

        return Accepted();
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["X-Refresh-Token"];
        var accessToken = Request.Cookies["X-Access-Token"];

        var command = new RefreshTokensCommand(accessToken, refreshToken);

        var response = await _sender.Send(command, cancellationToken);

        if (response.IsSuccess)
        {
            HttpContext.Response.Cookies.Append("X-Access-Token", response.Value.AccessToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,

            });

            HttpContext.Response.Cookies.Append("X-Refresh-Token", response.Value.RefreshToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,

            });
        }

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [AllowAnonymous]
    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthRequest request, CancellationToken cancellationToken)
    {

        var command = new GoogleLoginCommand(request.IdToken);

        var response = await _sender.Send(command, cancellationToken);

        if (response.IsSuccess)
        {
            HttpContext.Response.Cookies.Append("X-Access-Token", response.Value.AccessToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,

            });

            HttpContext.Response.Cookies.Append("X-Refresh-Token", response.Value.RefreshToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,

            });
        }

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("X-Access-Token", new CookieOptions()
        {

            HttpOnly = true,
            Secure = true
        }); ;

        HttpContext.Response.Cookies.Delete("X-Refresh-Token", new CookieOptions()
        {
            HttpOnly = true,
            Secure = true
        });

        return Ok();
    }

}