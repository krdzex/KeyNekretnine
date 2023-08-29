using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAdverts;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAgents;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyById;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyLocation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;

namespace KeyNekretnine.Api.Controllers.Agency;

[ApiController]
[Route("api/[controller]")]
public class AgencyController : ControllerBase
{
    private readonly ISender _sender;
    public AgencyController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{agencyId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAgencyById(int agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyByIdQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound();
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAgencies([FromQuery] AgencyParameters agencyParameters, CancellationToken cancellationToken)
    {
        var query = new GetAgenciesQuery(agencyParameters);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("{agencyId}/adverts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAgencyAdverts(int agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyAdvertsQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("{agencyId}/agents")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAgents(int agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyAgentsQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("{agencyId}/location")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAgencyLocation(int agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyLocationQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}