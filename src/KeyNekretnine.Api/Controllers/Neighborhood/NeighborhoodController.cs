using KeyNekretnine.Application.Core.Neighborhoods.Queries.GetNeighborhoodsByCityId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Api.Controllers.Neighborhood;

[ApiController]
[Route("api/[controller]")]
public class NeighborhoodController : ControllerBase
{
    private readonly ISender _sender;
    public NeighborhoodController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{cityId}")]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(int cityId, CancellationToken cancellationToken)
    {
        var query = new GetNeighborhoodsByCityIdQuery(cityId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}