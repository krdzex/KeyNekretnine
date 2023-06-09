﻿using Application.Commands.AuthCommands;
using Application.Core.Auth.Commands.UserRegistration;
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
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthenticationDto)
    {
        var tokens = await Sender.Send(new LoginUserCommand(userForAuthenticationDto));

        HttpContext.Response.Cookies.Append("X-Access-Token", tokens.AccessToken,
            new CookieOptions
            {
                HttpOnly = true
            });

        HttpContext.Response.Cookies.Append("X-Refresh-Token", tokens.RefreshToken,
            new CookieOptions
            {
                HttpOnly = true
            });

        return Accepted();
    }


    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
    {
        var tokens = await Sender.Send(new GoogleLoginCommand(googleLoginDto));

        HttpContext.Response.Cookies.Append("X-Access-Token", tokens.AccessToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });

        HttpContext.Response.Cookies.Append("X-Refresh-Token", tokens.RefreshToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
            });
        return Accepted();
    }

    [HttpPost("facebook-login")]
    public async Task<IActionResult> FacebookLogin([FromBody] string accessToken)
    {
        var tokens = await Sender.Send(new FacebookLoginCommand(accessToken));

        HttpContext.Response.Cookies.Append("X-Access-Token", tokens.AccessToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });

        HttpContext.Response.Cookies.Append("X-Refresh-Token", tokens.RefreshToken,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
            });

        return Accepted();
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

