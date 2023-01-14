using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet("current")]
    public async Task<IActionResult> Information()
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        return Ok(await _sender.Send(new GetCurrentUserQuery(email)));
    }
}
