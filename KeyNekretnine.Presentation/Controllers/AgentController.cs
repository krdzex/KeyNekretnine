//using Application.Core.Agents.Commands.CreateAgent;
//using Application.Core.Agents.Commands.UpdateAgent;
//using Application.Core.Agents.Queries.GetAgentAdverts;
//using Application.Core.Agents.Queries.GetAgentById;
//using Application.Core.Agents.Queries.GetAgents;
//using KeyNekretnine.Presentation.ActionFilters;
//using KeyNekretnine.Presentation.Infrastructure;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Shared.DataTransferObjects.Agency;
//using Shared.RequestFeatures;
//using System.Security.Claims;

//namespace KeyNekretnine.Presentation.Controllers;

//[Route("api/[controller]")]
//public class AgentController : ApiController
//{
//    public AgentController(ISender sender)
//        : base(sender)
//    {
//    }

//    [Authorize]
//    [ServiceFilter(typeof(BanUserChack))]
//    [HttpPost]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> Create([FromForm] CreateAgentDto createAgentDto, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var command = new CreateAgentCommand(createAgentDto, email);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);
//    }


//    [HttpGet]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> GetAll([FromQuery] AgentParameters agentParameters, CancellationToken cancellationToken)
//    {

//        var command = new GetAgentsQuery(agentParameters);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [HttpGet("{agentId}")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> GetAgent(int agentId, CancellationToken cancellationToken)
//    {
//        var query = new GetAgentByIdQuery(agentId);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [HttpGet("{agentId}/adverts")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> GetAgentAdverts(int agentId, CancellationToken cancellationToken)
//    {
//        var query = new GetAgentAdvertsQuery(agentId);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [Authorize]
//    [ServiceFilter(typeof(BanUserChack))]
//    [HttpPut("{agentId}")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> UpdateAgent([FromForm] UpdateAgentDto updateAgentDto, int agentId, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var command = new UpdateAgentCommand(updateAgentDto, agentId, email);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? Accepted() : HandleFailure(response);
//    }
//}