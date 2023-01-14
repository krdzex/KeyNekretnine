using Application.Commands;
using MediatR;
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
    public async Task<IActionResult> Refresh([FromBody] TokenRequest request)
    {
        return Ok(await _sender.Send(new CreateAccessAndRefreshTokenCommand(request)));

    }
};
