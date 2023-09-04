using KeyNekretnine.Application.Core.AdvertStatuses.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Api.Controllers.AdvertStatus;

[ApiController]
[Route("api/advert/statuses")]
public class AdvertStatusController : ControllerBase
{
    private readonly ISender _sender;
    public AdvertStatusController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetAdvertStatusesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}
