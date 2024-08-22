using KeyNekretnine.Application.Core.Agents.Commands.CreateAgent;
using KeyNekretnine.Application.Core.Agents.Commands.RemoveAgentImage;
using KeyNekretnine.Application.Core.Agents.Commands.UpdateAgent;
using KeyNekretnine.Application.Core.Agents.Queries.GetAgentAdverts;
using KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
using KeyNekretnine.Application.Core.Agents.Queries.GetPagedAgents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KeyNekretnine.Api.Controllers.Agent;

[ApiController]
[EnableRateLimiting("high-rating")]
[Route("api/[controller]")]
public class AgentController : ControllerBase
{
    private readonly ISender _sender;
    public AgentController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Creates a new agent.
    /// </summary>
    [Authorize]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateAgentRequest createAgentRequest, CancellationToken cancellationToken)
    {
        var command = new CreateAgentCommand(
            createAgentRequest.AgencyId,
            createAgentRequest.FirstName,
            createAgentRequest.LastName,
            createAgentRequest.Description,
            createAgentRequest.Email,
            createAgentRequest.PhoneNumber,
            createAgentRequest.TwitterUrl,
            createAgentRequest.FacebookUrl,
            createAgentRequest.InstagramUrl,
            createAgentRequest.LinkedinUrl,
            createAgentRequest.Image,
            createAgentRequest.LanguageIds);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Retrieves a paginated list of agents based on specified pagination parameters.
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] AgentPaginationParameters agentPaginationParameters, CancellationToken cancellationToken)
    {

        var command = new GetPagedAgentsQuery(
            agentPaginationParameters.OrderBy,
            agentPaginationParameters.PageNumber,
            agentPaginationParameters.PageSize);

        var response = await _sender.Send(command, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Retrieves an agent by its ID.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{agentId}")]
    public async Task<IActionResult> GetById(Guid agentId, CancellationToken cancellationToken)
    {
        var query = new GetAgentByIdQuery(agentId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    /// <summary>
    /// Retrieves advertisements associated with a specific agent by their agent ID.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{agentId}/adverts")]
    public async Task<IActionResult> GetAgentAdverts(Guid agentId, CancellationToken cancellationToken)
    {
        var query = new GetAgentAdvertsQuery(agentId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Updates an existing agent by its Id.
    /// </summary>
    [Authorize]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpPut("{agentId}")]
    public async Task<IActionResult> Update([FromForm] UpdateAgentRequest updateAgentRequest, Guid agentId, CancellationToken cancellationToken)
    {
        var command = new UpdateAgentCommand(
            agentId,
            updateAgentRequest.FirstName,
            updateAgentRequest.LastName,
            updateAgentRequest.Description,
            updateAgentRequest.Email,
            updateAgentRequest.TwitterUrl,
            updateAgentRequest.FacebookUrl,
            updateAgentRequest.InstagramUrl,
            updateAgentRequest.LinkedinUrl,
            updateAgentRequest.PhoneNumber,
            updateAgentRequest.Image,
            updateAgentRequest.LanguageIds);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Accepted() : BadRequest(response.Error);
    }


    /// <summary>
    /// Remove image of a specific agent by its ID.
    /// </summary>
    [Authorize]
    [HttpDelete("{agentId}/image")]
    public async Task<IActionResult> RemoveAgencyImage(Guid agentId, CancellationToken cancellationToken)
    {
        var command = new RemoveAgentImageCommand(agentId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Accepted() : BadRequest(response.Error);
    }
}
