using KeyNekretnine.Application.Core.RejectReasons.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Api.Controllers.RejectReason;

[ApiController]
[Route("api/reject-reason")]
public class RejectReasonController : ControllerBase
{
    private readonly ISender _sender;
    public RejectReasonController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetRejectReasonsQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}

