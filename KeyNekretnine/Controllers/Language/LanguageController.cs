using KeyNekretnine.Application.Core.Language.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Api.Controllers.Language;

[ApiController]
[Route("api/[controller]")]
public class LanguageController : ControllerBase
{
    private readonly ISender _sender;
    public LanguageController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetLanguagesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}