using Application.Core.Neighborhoods.Queries;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
public class NeighborhoodController : ApiController
{
    public NeighborhoodController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{cityId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(int cityId, CancellationToken cancellationToken)
    {
        var query = new GetNeighborhoodsByCityIdQuery(cityId);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }
}