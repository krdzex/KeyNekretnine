using Application.Core.Tokens.Commands.RefreshTokens;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;

namespace KeyNekretnine.Presentation.Controllers;
[Route("api/token")]
[ApiController]
public class TokenController : ApiController
{

    public TokenController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
    {
        var newTokens = new TokenRequest { RefreshToken = Request.Cookies["X-Refresh-Token"], AccessToken = Request.Cookies["X-Access-Token"] };

        var command = new RefreshTokensCommand(newTokens);

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

        return response.IsSuccess ? NoContent() : HandleFailure(response);
    }
};