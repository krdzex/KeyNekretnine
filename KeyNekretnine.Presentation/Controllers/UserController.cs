using Application.Core.Users.Commands.MultipleUsersUnban;
using Application.Core.Users.Commands.UnbanUser;
using Application.Core.Users.Commands.UpdateUser;
using Application.Core.Users.Notifications.BanUser;
using Application.Core.Users.Notifications.MultipleUserBan;
using Application.Core.Users.Queries.ConfirmEmailQuery;
using Application.Core.Users.Queries.GetCurrentUserQuery;
using Application.Core.Users.Queries.GetUserByQuery;
using Application.Core.Users.Queries.GetUsersQuery;
using KeyNekretnine.Attributes;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
public sealed class UserController : ApiController
{

    public UserController(ISender sender)
        : base(sender)
    {
    }

    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpGet("current")]
    public async Task<IActionResult> Information(CancellationToken cancellationToken)
    {
        var userClaims = User.Claims;

        var query = new GetCurrentUserQuery(userClaims);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("ban")]
    public async Task<IActionResult> Ban([FromBody] BanUserDto banUserDto, CancellationToken cancellationToken)
    {
        var command = new BanUserCommand(banUserDto.Email, banUserDto.Days);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : HandleFailure(response);
    }

    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("unban")]
    public async Task<IActionResult> Unban([FromBody] string email, CancellationToken cancellationToken)
    {
        var command = new UnbanUserCommand(email);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : HandleFailure(response);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("/api/users")]
    public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters, CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery(userParameters);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, string email, CancellationToken cancellationToken)
    {
        var query = new ConfirmUserEmailCommand(token, email);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? RedirectToPage("https://keynekretnine-dev.vercel.app") : HandleFailure(response);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("multiple/ban")]
    public async Task<IActionResult> MultipleBan([FromBody] BanUsersDto banUsers, CancellationToken cancellationToken)
    {
        var command = new MultipleUsersBanCommand(banUsers.Emails, banUsers.Days);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : HandleFailure(response);
    }

    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("multiple/unban")]
    public async Task<IActionResult> MultipleUnban([FromBody] List<string> userIds, CancellationToken cancellationToken)
    {
        var command = new MultipleUsersUnbanCommand(userIds);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : HandleFailure(response);

    }

    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var command = new UpdateUserCommand(updateUserDto, email);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : HandleFailure(response);
    }
}
