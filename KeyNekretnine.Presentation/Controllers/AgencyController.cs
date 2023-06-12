using Application.Core.Agencies.Commands.CreateAgency;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Agency;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
public class AgencyController : ApiController
{

    public AgencyController(ISender sender)
        : base(sender)
    {
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAgency([FromBody] CreateAgencyDto createAgencyDto, CancellationToken cancellationToken)
    {
        var command = new CreateAgencyCommand(createAgencyDto);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : HandleFailure(response);
    }
}