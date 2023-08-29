using KeyNekretnine.Application.Core.AdvertPurposes.Queries.GetAdvertPurposes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Api.Controllers.AdvertPurpose;

[ApiController]
[Route("api/advert/purposes")]
public class AdvertPurposeController : ControllerBase
{
    private readonly ISender _sender;

    public AdvertPurposeController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetAdvertPurposesQuery();

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
