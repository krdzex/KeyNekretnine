using KeyNekretnine.Application.Core.AdvertTypes.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Api.Controllers.AdvertType;

[ApiController]
[Route("api/advert/types")]
public class AdvertTypeController : ControllerBase
{
    private readonly ISender _sender;
    public AdvertTypeController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetAdvertTypesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}

