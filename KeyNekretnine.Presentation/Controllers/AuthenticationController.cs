using Application.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Auth;

namespace KeyNekretnine.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userForRegistration)
    {
        await _sender.Send(new RegisterUserCommand(userForRegistration));

        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthenticationDto)
    {
        var tokens = await _sender.Send(new LoginUserCommand(userForAuthenticationDto));

        return Accepted(tokens);
    }
}

