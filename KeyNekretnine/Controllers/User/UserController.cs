using KeyNekretnine.Application.Core.Users.Commands.BanUser;
using KeyNekretnine.Application.Core.Users.Queries.Get;
using KeyNekretnine.Application.Core.Users.Queries.GetByIdQuery;
using KeyNekretnine.Application.Core.Users.Queries.GetCurrent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KeyNekretnine.Api.Controllers.User;

[ApiController]
[Route("api/[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly ISender _sender;
    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpGet("current")]
    public async Task<IActionResult> CurrentUser(CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var query = new GetCurrentUserQuery(userId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }


    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserPaginationParameters userPaginationParameters, CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery(
            userPaginationParameters.OrderBy,
            userPaginationParameters.PageNumber,
            userPaginationParameters.PageSize,
            userPaginationParameters.Username,
            userPaginationParameters.IsBanned);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [Authorize(Roles = "Administrator")]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpPut("ban")]
    public async Task<IActionResult> Ban([FromBody] BanUserRequest banUserRequest, CancellationToken cancellationToken)
    {
        var command = new BanUserCommand(banUserRequest.UserId, banUserRequest.Days);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : NotFound(response.Error);
    }
}
