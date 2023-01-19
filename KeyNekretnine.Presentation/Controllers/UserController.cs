using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;
using System.ComponentModel.DataAnnotations;
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

    //[Authorize]
    [HttpPut("{id:guid}/ban")]
    public async Task<IActionResult> Ban(Guid id, [Required] int days)
    {
        await _sender.Send(new BanUserCommand(id.ToString(), days));

        return Ok();
    }

    //[Authorize]
    [HttpPut("{id:guid}/unban")]
    public async Task<IActionResult> Unban(Guid id)
    {
        await _sender.Send(new UnbanUserCommand(id.ToString()));

        return Ok();
    }

    [HttpGet("/api/users")]
    public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters)
    {
        var users = await _sender.Send(new GetUsersQuery(userParameters));

        return Ok(users);
    }

    [HttpGet("/api/users/banned")]
    public async Task<IActionResult> GetBannedUsers([FromQuery] UserParameters userParameters)
    {
        var bannedUsers = await _sender.Send(new GetBannedUsersQuery(userParameters));

        return Ok(bannedUsers);
    }
}
