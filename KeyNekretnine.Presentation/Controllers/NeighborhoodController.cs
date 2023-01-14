using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NeighborhoodController : ControllerBase
{

    private readonly ISender _sender;

    public NeighborhoodController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _sender.Send(new GetNeighborhoodsQuery(id)));
    }
}
