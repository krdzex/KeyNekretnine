using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;

namespace KeyNekretnine.Presentation.Controllers;
[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ISender _sender;

    public TokenController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh([FromBody] TokenRequest request)
    {
        var refreshToken = Request.Cookies["mjoifp-fo8ahsj"];
        var accessToken = Request.Cookies["xcvuhgi-awtzpdsa"];

        var newTokens = new TokenRequest { RefreshToken = refreshToken, Token = accessToken };
        var tokens = await _sender.Send(new CreateAccessAndRefreshTokenCommand(newTokens));

        HttpContext.Response.Cookies.Append("xcvuhgi-awtzpdsa", tokens.Token,
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,

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
        return Ok(tokens);

    }
};