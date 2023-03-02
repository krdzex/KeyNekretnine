using Application.Queries.CityQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly ISender _sender;

    public CityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _sender.Send(new GetCitiesQuery()));
    }

    [HttpGet("popular")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> GetMostPopular()
    {
        return Ok(await _sender.Send(new GetMostPopularCitiesQuery()));
    }
}