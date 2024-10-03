using KeyNekretnine.Application.Core.Agencies.Commands.CreateAgency;
using KeyNekretnine.Application.Core.Agencies.Commands.RemoveAgencyImage;
using KeyNekretnine.Application.Core.Agencies.Commands.UpdateAgency;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAdverts;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyAgents;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyById;
using KeyNekretnine.Application.Core.Agencies.Queries.GetAgencyLocation;
using KeyNekretnine.Application.Core.Agencies.Queries.GetPagedAgencies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KeyNekretnine.Api.Controllers.Agency;

[ApiController]
[EnableRateLimiting("high-rating")]
[Route("api/[controller]")]
public class AgencyController : ControllerBase
{
    private readonly ISender _sender;
    public AgencyController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Creates a new agency.
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAgencyRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAgencyCommand(
            request.Email,
            request.Password,
            request.AgencyName);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    /// <summary>
    /// Updates an existing agency by its Id.
    /// </summary>
    [Authorize]
    [HttpPut("{agencyId}")]
    public async Task<IActionResult> Update([FromForm] UpdateAgencyRequest request, Guid agencyId, CancellationToken cancellationToken)
    {
        var command = new UpdateAgencyCommand(
            agencyId,
            request.AgencyName,
            request.Address,
            request.Description,
            request.Email,
            request.WebsiteUrl,
            request.WorkStartTime,
            request.WorkEndTime,
            request.TwitterUrl,
            request.FacebookUrl,
            request.InstagramUrl,
            request.LinkedinUrl,
            request.Latitude,
            request.Longitude,
            request.PhoneNumber,
            request.LanguageIds,
            request.Image);


        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Accepted() : BadRequest(response.Error);
    }

    /// <summary>
    /// Retrieves an agency by its ID.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{agencyId}")]
    public async Task<IActionResult> GetById(Guid agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyByIdQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    /// <summary>
    /// Retrieves a paginated list of agencies based on specified filtering and pagination parameters.
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] AgencyPaginationParameters request, CancellationToken cancellationToken)
    {
        var query = new GetPagedAgenciesQuery(
            request.OrderBy,
            request.PageNumber,
            request.PageSize,
            request.Name);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Retrieves advertisements associated with a specific agency by its ID.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{agencyId}/adverts")]
    public async Task<IActionResult> GetAgencyAdverts(Guid agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyAdvertsQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Retrieves agents associated with a specific agency by its ID.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{agencyId}/agents")]
    public async Task<IActionResult> GetAgents(Guid agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyAgentsQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Retrieves the location of a specific agency by its ID.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{agencyId}/location")]
    public async Task<IActionResult> GetAgencyLocation(Guid agencyId, CancellationToken cancellationToken)
    {
        var query = new GetAgencyLocationQuery(agencyId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Remove image of a specific agency by its ID.
    /// </summary>
    [Authorize]
    [HttpDelete("{agencyId}/image")]
    public async Task<IActionResult> RemoveAgencyImage(Guid agencyId, CancellationToken cancellationToken)
    {
        var command = new RemoveAgencyImageCommand(agencyId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Accepted() : BadRequest(response.Error);
    }
}