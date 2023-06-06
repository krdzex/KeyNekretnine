using Application.Commands.UserCommands;
using Application.Core.Users.Queries.ConfirmEmailQuery;
using Application.Core.Users.Queries.GetCurrentUserQuery;
using Application.Core.Users.Queries.GetUserByQuery;
using Application.Core.Users.Queries.GetUsersQuery;
using Application.Notifications.UserNotifications;
using KeyNekretnine.Attributes;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
public sealed class UserController : ApiController
{
    private readonly IPublisher _publisher;

    public UserController(ISender sender, IPublisher published)
        : base(sender)
    {
        _publisher = published;
    }

    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpGet("current")]
    public async Task<IActionResult> Information(CancellationToken cancellationToken)
    {
        var userClaims = User.Claims;

        var query = new GetCurrentUserQuery(userClaims);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("{id:guid}/ban")]
    public async Task<IActionResult> Ban(Guid id, [Required] int days)
    {
        await _publisher.Publish(new BanUserNotification(id.ToString(), days));

        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("{id:guid}/unban")]
    public async Task<IActionResult> Unban(Guid id)
    {
        await _publisher.Publish(new UnbanUserNotification(id.ToString()));

        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("/api/users")]
    public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters, CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery(userParameters);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, string email, CancellationToken cancellationToken)
    {
        var query = new ConfirmUserEmailCommand(token, email);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? RedirectToPage("https://keynekretnine-dev.vercel.app") : BadRequest(response.Error);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("multiple/ban")]
    public async Task<IActionResult> MultipleBan([FromBody] BanUsersDto banUsers)
    {
        await _publisher.Publish(new MultipleBanUserNotification(banUsers.UserIds, banUsers.Days));

        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("multiple/unban")]
    public async Task<IActionResult> MultipleUnban([FromBody] List<string> userIds)
    {
        await _publisher.Publish(new MultipleUnbanUserNotification(userIds));

        return NoContent();
    }

    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto updateUserDto)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        await Sender.Send(new UpdateUserCommand(updateUserDto, email));

        return NoContent();
    }
}
