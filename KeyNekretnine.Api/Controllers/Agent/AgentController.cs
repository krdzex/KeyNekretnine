using KeyNekretnine.Application.Core.Agents.Commands.CreateAgent;
using KeyNekretnine.Application.Core.Agents.Commands.UpdateAgent;
using KeyNekretnine.Application.Core.Agents.Queries.GetAgentAdverts;
using KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
using KeyNekretnine.Application.Core.Agents.Queries.GetAgents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KeyNekretnine.Api.Controllers.Agent;

[ApiController]
[Route("api/[controller]")]
public class AgentController : ControllerBase
{
    private readonly ISender _sender;
    public AgentController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateAgentRequest createAgentRequest, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var command = new CreateAgentCommand(
            createAgentRequest.AgencyId,
            userId,
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

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] AgentPaginationParameters agentPaginationParameters, CancellationToken cancellationToken)
    {

        var command = new GetPagedAgentsQuery(
            agentPaginationParameters.OrderBy,
            agentPaginationParameters.PageNumber,
            agentPaginationParameters.PageSize);

        var response = await _sender.Send(command, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("{agentId}")]
    public async Task<IActionResult> GetById(Guid agentId, CancellationToken cancellationToken)
    {
        var query = new GetAgentByIdQuery(agentId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet("{agentId}/adverts")]
    public async Task<IActionResult> GetAgentAdverts(Guid agentId, CancellationToken cancellationToken)
    {
        var query = new GetAgentAdvertsQuery(agentId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [Authorize]
    //[ServiceFilter(typeof(BanUserChack))]
    [HttpPut("{agentId}")]
    public async Task<IActionResult> UpdateAgent([FromForm] UpdateAgentRequest updateAgentRequest, Guid agentId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var command = new UpdateAgentCommand(
            agentId,
            userId,
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

        return response.IsSuccess ? Accepted() : BadRequest(response);
    }
}
