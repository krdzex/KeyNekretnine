using KeyNekretnine.Application.Core.Language.Queries.GetLanguages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KeyNekretnine.Api.Controllers.Language;

[ApiController]
[EnableRateLimiting("high-rating")]
[Route("api/[controller]")]
public class LanguageController : ControllerBase
{
    private readonly ISender _sender;
    public LanguageController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [AllowAnonymous]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetLanguagesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}