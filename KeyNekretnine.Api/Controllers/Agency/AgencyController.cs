using KeyNekretnine.Application.Core.Agencies.Commands.CreateAgency;
using KeyNekretnine.Application.Core.Agencies.Commands.UpdateAgency;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencies;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAdverts;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAgents;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyById;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyLocation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpPost]
    public async Task<IActionResult> CreateAgency([FromBody] CreateAgencyRequest createAgencyRequest, CancellationToken cancellationToken)
    {
        var command = new CreateAgencyCommand(
            createAgencyRequest.FirstName,
            createAgencyRequest.LastName,
            createAgencyRequest.UserName,
            createAgencyRequest.Email,
            createAgencyRequest.Password,
            createAgencyRequest.AgencyName);

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }

    [Authorize]
    [HttpPut("{agencyId}")]
    public async Task<IActionResult> UpdateAgency([FromForm] UpdateAgencyRequest updateAgencyRequest, Guid agencyId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var command = new UpdateAgencyCommand(
            agencyId,
            updateAgencyRequest.AgencyName,
            userId,
            updateAgencyRequest.Address,
            updateAgencyRequest.Description,
            updateAgencyRequest.Email,
            updateAgencyRequest.WebsiteUrl,
            updateAgencyRequest.WorkStartTime,
            updateAgencyRequest.WorkEndTime,
            updateAgencyRequest.TwitterUrl,
            updateAgencyRequest.FacebookUrl,
            updateAgencyRequest.InstagramUrl,
            updateAgencyRequest.LinkedinUrl,
            updateAgencyRequest.Latitude,
            updateAgencyRequest.Longitude,
            updateAgencyRequest.LanguageIds,
            updateAgencyRequest.Image);


        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Accepted() : BadRequest(response.Error);
    }

    [HttpGet("{agencyId}")]
    public async Task<IActionResult> GetAgencyById(Guid agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyByIdQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }


    [HttpGet]
    public async Task<IActionResult> GetAgencies([FromQuery] AgencyPaginationParameters agencyPaginationParameters, CancellationToken cancellationToken)
    {
        var query = new GetAgenciesQuery(
            agencyPaginationParameters.OrderBy,
            agencyPaginationParameters.PageNumber,
            agencyPaginationParameters.PageSize,
            agencyPaginationParameters.Name);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("{agencyId}/adverts")]
    public async Task<IActionResult> GetAgencyAdverts(Guid agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyAdvertsQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("{agencyId}/agents")]
    public async Task<IActionResult> GetAgents(Guid agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyAgentsQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("{agencyId}/location")]
    public async Task<IActionResult> GetAgencyLocation(Guid agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyLocationQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}