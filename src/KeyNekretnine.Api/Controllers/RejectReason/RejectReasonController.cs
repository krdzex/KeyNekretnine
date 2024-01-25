using KeyNekretnine.Application.Core.RejectReasons.Queries.GetRejectReasons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KeyNekretnine.Api.Controllers.RejectReason;

[ApiController]
[EnableRateLimiting("high-rating")]
[Route("api/reject-reason")]
public class RejectReasonController : ControllerBase
{
    private readonly ISender _sender;
    public RejectReasonController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [AllowAnonymous]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetRejectReasonsQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}

