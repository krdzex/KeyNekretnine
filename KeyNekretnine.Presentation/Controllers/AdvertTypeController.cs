using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/advert/types")]
[ApiController]
public class AdvertTypeController : ControllerBase
{
    private readonly ISender _sender;

    public AdvertTypeController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _sender.Send(new GetAdvertTypesQuery()));
    }
}
