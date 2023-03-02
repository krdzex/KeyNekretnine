using Application.Commands.UserCommands;
using Application.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Auth;

namespace KeyNekretnine.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;
    public AuthenticationController(ISender sender, IPublisher publisher)
    {
        _sender = sender;
        _publisher = publisher;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userForRegistration)
    {
        await _publisher.Publish(new UserSignupNotification(userForRegistration));

        return StatusCode(201);
    }

    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthenticationDto)
    {
        var tokens = await _sender.Send(new LoginUserCommand(userForAuthenticationDto));

        return Accepted(tokens);
    }
}