using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using KeyNekretnine.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;
using System.ComponentModel.DataAnnotations;

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
    [ServiceFilter(typeof(BanUserChack))]
    [HttpGet("current")]
    public async Task<IActionResult> Information()
    {
        var userClaims = User.Claims;

        return Ok(await _sender.Send(new GetCurrentUserQuery(userClaims)));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id:guid}/ban")]
    public async Task<IActionResult> Ban(Guid id, [Required] int days)
    {
        await _sender.Send(new BanUserCommand(id.ToString(), days));

        return Ok();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id:guid}/unban")]
    public async Task<IActionResult> Unban(Guid id)
    {
        await _sender.Send(new UnbanUserCommand(id.ToString()));

        return Ok();
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("/api/users")]
    public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters)
    {
        var users = await _sender.Send(new GetUsersQuery(userParameters));

        return Ok(users);
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, string email)
    {
        await _sender.Send(new ConfirmUserEmailQuery(token, email));

        return Ok();
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        var user = await _sender.Send(new GetUsersQuery(userParameters));

        return Ok();
    }
}
