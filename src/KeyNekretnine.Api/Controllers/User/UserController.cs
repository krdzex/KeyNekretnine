using KeyNekretnine.Application.Core.Users.Commands.BanUser;
using KeyNekretnine.Application.Core.Users.Commands.ChangeUserPassword;
using KeyNekretnine.Application.Core.Users.Commands.ConfirmUserEmail;
using KeyNekretnine.Application.Core.Users.Commands.PasswordForgot;
using KeyNekretnine.Application.Core.Users.Commands.RequestEmailConfirmation;
using KeyNekretnine.Application.Core.Users.Commands.RequestPasswordForgot;
using KeyNekretnine.Application.Core.Users.Commands.UnbanUser;
using KeyNekretnine.Application.Core.Users.Commands.UpdateUser;
using KeyNekretnine.Application.Core.Users.Queries.GetAboutUser;
using KeyNekretnine.Application.Core.Users.Queries.GetCurrentUser;
using KeyNekretnine.Application.Core.Users.Queries.GetUserById;
using KeyNekretnine.Application.Core.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KeyNekretnine.Api.Controllers.User;

[ApiController]
[EnableRateLimiting("high-rating")]
[Route("api/[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly ISender _sender;
    public UserController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Retrieves the current logged-in user information.
    /// </summary>
    [Authorize]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
    {
        var query = new GetCurrentUserQuery();

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    /// <summary>
    /// Retrieves a paginated list of users based on specified pagination parameters.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] UserPaginationParameters userPaginationParameters, CancellationToken cancellationToken)
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

    /// <summary>
    /// Retrieves a user by their ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    /// <summary>
    /// Bans a user for a specified number of days for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpPut("ban")]
    public async Task<IActionResult> Ban([FromBody] BanUserRequest banUserRequest, CancellationToken cancellationToken)
    {
        var command = new BanUserCommand(banUserRequest.UserId, banUserRequest.Days);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : NotFound(response.Error);
    }

    /// <summary>
    /// Unbans a user for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpPut("unban")]
    public async Task<IActionResult> Unban([FromBody] string userId, CancellationToken cancellationToken)
    {
        var command = new UnbanUserCommand(userId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : NotFound(response.Error);
    }

    /// <summary>
    /// Updates the current authenticated user's information.
    /// </summary>
    [Authorize]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequest updateUserRequest, CancellationToken cancellationToken)
    {
        var query = new UpdateUserCommand(
            updateUserRequest.About,
            updateUserRequest.FirstName,
            updateUserRequest.LastName,
            updateUserRequest.Image);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Accepted() : BadRequest(response.Error);
    }

    /// <summary>
    /// Requests email confirmation for the current authenticated user.
    /// </summary>
    [Authorize]
    [EnableRateLimiting("low-rating")]
    [HttpPost("request-email-confirmation")]
    public async Task<IActionResult> RequestEmailConfirmation(CancellationToken cancellationToken)
    {
        var command = new RequestEmailConfirmationCommand();

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    /// <summary>
    /// Confirms the email of a user using the provided token and email address.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token, string email, CancellationToken cancellationToken)
    {
        var query = new ConfirmUserEmailCommand(token, email);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? RedirectToPage("https://testing-ui.keynekretnine.me") : BadRequest(response.Error);
    }

    /// <summary>
    /// Changes the password for the current authenticated user.
    /// </summary>
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var command = new ChangeUserPasswordCommand(request.CurrentPassword, request.NewPassword);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Requests a password reset link for the provided email address.
    /// </summary>
    [EnableRateLimiting("low-rating")]
    [HttpPost("request-password-forgot")]
    public async Task<IActionResult> RequestPasswordForgot([FromBody] PasswordForgotLinkRequest request, CancellationToken cancellationToken)
    {
        var command = new RequestPasswordForgotCommand(request.Email);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Resets the password for the provided email address using the token and new password.
    /// </summary>
    [HttpPost("password-forgot")]
    public async Task<IActionResult> ForgotPassword([FromBody] PasswordForgotRequest request, CancellationToken cancellationToken)
    {
        var command = new PasswordForgotCommand(request.Email, request.Token, request.NewPassword);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Retrieves information about the current  authenticated user.
    /// </summary>
    [Authorize]
    [HttpGet("about")]
    public async Task<IActionResult> AboutUser(CancellationToken cancellationToken)
    {
        var query = new GetAboutUserQuery();

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }
}
