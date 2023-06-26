using Application.Core.Agencies.Commands.CreateAgency;
using Application.Core.Agencies.Commands.CreateImaginaryAgent;
using Application.Core.Agencies.Commands.UpdateAgency;
using Application.Core.Agencies.Queries.GetAgencies;
using Application.Core.Agencies.Queries.GetAgencyAdverts;
using Application.Core.Agencies.Queries.GetAgencyById;
using KeyNekretnine.Attributes;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Agency;
using Shared.RequestFeatures;
using System.Security.Claims;

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

    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPost("{agencyId}/agent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAgent([FromForm] ImaginaryAgentDto imaginaryAgentDto, int agencyId, CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var command = new CreateImaginaryAgentCommand(imaginaryAgentDto, email, agencyId);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : HandleFailure(response);
    }

    [HttpGet("{agencyId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAgencyById(int agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyByIdQuery(agencyId);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAgencies([FromQuery] AgencyParameters agencyParameters, CancellationToken cancellationToken)
    {
        var query = new GetAgenciesQuery(agencyParameters);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [HttpPut("{agencyId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAgency([FromForm] UpdateAgencyDto updateAgencyDto, int agencyId, CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var command = new UpdateAgencyCommand(updateAgencyDto, agencyId, email);

        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? Accepted() : HandleFailure(response);
    }

    [HttpGet("{agencyId}/adverts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAgencyAdverts(int agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyAdvertsQuery(agencyId);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }
}