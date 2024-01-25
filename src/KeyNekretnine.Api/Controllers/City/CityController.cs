using KeyNekretnine.Application.Core.Cities.Queries.GetCities;
using KeyNekretnine.Application.Core.Cities.Queries.GetMostPopularCities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KeyNekretnine.Api.Controllers.City;

[ApiController]
[EnableRateLimiting("high-rating")]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ISender _sender;
    public CityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [AllowAnonymous]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetCitiesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("popular")]
    [AllowAnonymous]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> GetMostPopular(CancellationToken cancellationToken)
    {
        var query = new GetMostPopularCitiesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}