using Application.Core.Auth.Commands.FacebookLogin;
using Application.Core.Auth.Commands.GoogleLogin;
using Application.Core.Auth.Commands.UserRegistration;
using Application.Core.Auth.Queries.UserLogin;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Auth;

namespace KeyNekretnine.Presentation.Controllers;
[Route("api/[controller]")]
public class AuthenticationController : ApiController
{

    public AuthenticationController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost("registration")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userForRegistration, CancellationToken cancellationToken)
    {
        var command = new UserRegistrationCommand(userForRegistration);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? StatusCode(201) : HandleFailure(response);
    }

    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthenticationDto, CancellationToken cancellationToken)
    {
        var query = new LoginUserQuery(userForAuthenticationDto);

        var response = await Sender.Send(query, cancellationToken);

        if (response.IsSuccess)
        {
            HttpContext.Response.Cookies.Append("X-Access-Token", response.Value.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true
                });

            HttpContext.Response.Cookies.Append("X-Refresh-Token", response.Value.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true
                });
        }

        return response.IsSuccess ? Accepted() : HandleFailure(response);
    }


    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto, CancellationToken cancellationToken)
    {

        var command = new GoogleLoginCommand(googleLoginDto);

        var response = await Sender.Send(command, cancellationToken);

        if (response.IsSuccess)
        {
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
                SameSite = SameSiteMode.None,
            });
        }

        return response.IsSuccess ? Accepted() : HandleFailure(response);
    }

    [HttpPost("facebook-login")]
    public async Task<IActionResult> FacebookLogin([FromBody] string accessToken, CancellationToken cancellationToken)
    {
        var command = new FacebookLoginCommand(accessToken);

        var response = await Sender.Send(command, cancellationToken);

        if (response.IsSuccess)
        {
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
                SameSite = SameSiteMode.None,
            });
        }

        return response.IsSuccess ? Accepted() : HandleFailure(response);
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

