using Application.Commands.AdvertCommands;
using Application.Queries.AdvertQuery;
using KeyNekretnine.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdvertController : ControllerBase
{
    private readonly ISender _sender;

    public AdvertController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _sender.Send(new GetAdvertQuery { Id = id, BypassCache = false }));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaging([FromQuery] AdvertParameters advertParameters)
    {
        return Ok(await _sender.Send(new GetAdvertsInPaginationQuery(advertParameters)));
    }

    [HttpGet("map")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMapPoints()
    {
        return Ok(await _sender.Send(new GetMapPointsQuery()));
    }

    [HttpGet("map/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdvertFromMapPoint(int id)
    {
        return Ok(await _sender.Send(new GetAdvertFromMapQuery(id)));
    }

    [Authorize]
    [HttpPost]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] AddAdvertDto newAdvert)
    {

        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        await _sender.Send(new CreateAdvertCommand(newAdvert, email));

        return Accepted();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{advertId}/approve")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Approve(int advertId)
    {

        await _sender.Send(new ApproveAdvertCommand(advertId));

        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{advertId}/decline")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Decline(int advertId)
    {

        await _sender.Send(new DeclineAdvertCommand(advertId));

        return NoContent();
    }
}
