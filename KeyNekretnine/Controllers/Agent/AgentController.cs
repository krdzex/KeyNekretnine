using KeyNekretnine.Application.Core.Agents.Queries.GetAgentAdverts;
using KeyNekretnine.Application.Core.Agents.Queries.GetAgentById;
using KeyNekretnine.Application.Core.Agents.Queries.GetAgents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] AgentPaginationParameters agentPaginationParameters, CancellationToken cancellationToken)
    {

        var command = new GetAgentsQuery(
            agentPaginationParameters.OrderBy,
            agentPaginationParameters.PageNumber,
            agentPaginationParameters.PageSize);

        var response = await _sender.Send(command, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("{agentId}")]
    public async Task<IActionResult> GetById(int agentId, CancellationToken cancellationToken)
    {
        var query = new GetAgentByIdQuery(agentId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet("{agentId}/adverts")]
    public async Task<IActionResult> GetAgentAdverts(int agentId, CancellationToken cancellationToken)
    {
        var query = new GetAgentAdvertsQuery(agentId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}
