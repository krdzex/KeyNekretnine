using Application.Core.Email.Commands.SendConfirmEmail;
using KeyNekretnine.Attributes;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
public class EmailController : ApiController
{

    public EmailController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost]
    [Authorize]
    [Route("/api/user/confirm")]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendEmailConfirm(CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var command = new SendConfirmEmailCommand(email);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
