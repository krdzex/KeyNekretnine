using Application.Core.Agencies.Commands.CreateAgent;
using KeyNekretnine.Attributes;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Agency;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
public class AgentController : ApiController
{
    public AgentController(ISender sender)
        : base(sender)
    {
    }

    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateAgentDto createAgentDto, CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var command = new CreateAgentCommand(createAgentDto, email);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : HandleFailure(response);
    }
}