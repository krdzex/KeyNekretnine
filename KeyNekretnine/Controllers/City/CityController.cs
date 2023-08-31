using KeyNekretnine.Application.Core.Cities.Queries.GetCities;
using KeyNekretnine.Application.Core.Cities.Queries.GetMostPopularCtities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Api.Controllers.City;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ISender _sender;
    public CityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetCitiesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("popular")]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> GetMostPopular(CancellationToken cancellationToken)
    {
        var query = new GetMostPopularCitiesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}