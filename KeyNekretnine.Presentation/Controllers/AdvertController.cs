using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;

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
        return Ok(await _sender.Send(new GetAdvertQuery(id)));
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
}
