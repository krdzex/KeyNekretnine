using Application.Commands.AuthCommands;
using Application.Notifications.AuthNotification;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Auth;

namespace KeyNekretnine.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;
    private readonly HttpClient _httpClient;

    public AuthenticationController(ISender sender, IPublisher publisher, HttpClient httpClient)
    {
        _sender = sender;
        _publisher = publisher;
        _httpClient = httpClient;
    }

    [HttpPost("registration")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userForRegistration)
    {
        await _publisher.Publish(new UserSignupNotification(userForRegistration));

        return StatusCode(201);
    }

    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthenticationDto)
    {
        var tokens = await _sender.Send(new LoginUserCommand(userForAuthenticationDto));

        HttpContext.Response.Cookies.Append("xcvuhgi-awtzpdsa", tokens.Token,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });

        HttpContext.Response.Cookies.Append("mjoifp-fo8ahsj", tokens.RefreshToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });

        return Accepted(tokens);
    }


    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
    {
        var tokens = await _sender.Send(new GoogleLoginCommand(googleLoginDto));

        HttpContext.Response.Cookies.Append("xcvuhgi-awtzpdsa", tokens.Token,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });

        HttpContext.Response.Cookies.Append("mjoifp-fo8ahsj", tokens.RefreshToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
            });
        return Accepted(tokens);
    }

    [HttpPost("facebook-login")]
    public async Task<IActionResult> FacebookLogin([FromBody] string accessToken)
    {
        var tokens = await _sender.Send(new FacebookLoginCommand(accessToken));

        HttpContext.Response.Cookies.Append("xcvuhgi-awtzpdsa", tokens.Token,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });

        HttpContext.Response.Cookies.Append("mjoifp-fo8ahsj", tokens.RefreshToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
            });

        return Accepted(tokens);
    }
}

