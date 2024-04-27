using KeyNekretnine.Application.Core.Neighborhoods.Queries.GetNeighborhoodsByCitySlug;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KeyNekretnine.Api.Controllers.Neighborhood;

[ApiController]
[EnableRateLimiting("high-rating")]
[Route("api/[controller]")]
public class NeighborhoodController : ControllerBase
{
    private readonly ISender _sender;
    public NeighborhoodController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Retrieves neighborhoods for a specific city by its slug.
    /// </summary>
    [HttpGet("{citySlug}")]
    [AllowAnonymous]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(string citySlug, CancellationToken cancellationToken)
    {
        var query = new GetNeighborhoodsByCitySlugQuery(citySlug);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}