using KeyNekretnine.Application.Core.Analytic.Queries.GetMiddleSectionAnalyticForAdvert;
using KeyNekretnine.Application.Core.Analytic.Queries.GetTopSectionAnalyticForAdvert;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KeyNekretnine.Api.Controllers.Analytic;
[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("high-rating")]
public class AnalyticController : ControllerBase
{
    private readonly ISender _sender;
    public AnalyticController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// You can search for Advert here using Reference Id.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{referenceId}/top-section")]
    public async Task<IActionResult> GetTopSectionAnalyticsForAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetTopSectionAnalyticForAdvertQuery(referenceId);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest();
    }

    /// <summary>
    /// You can search for Advert here using Reference Id.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{referenceId}/middle-section")]
    public async Task<IActionResult> GetMiddleSectionAnalyticsForAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetMiddleSectionAnalyticForAdvertQuery(referenceId);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest();
    }
}