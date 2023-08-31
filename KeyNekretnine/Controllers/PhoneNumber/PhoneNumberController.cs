using KeyNekretnine.Application.Core.PhoneNumbers.Queries.GetPhoneNumbers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Api.Controllers.PhoneNumber;

[ApiController]
[Route("api/phone-number")]
public class PhoneNumberController : ControllerBase
{
    private readonly ISender _sender;
    public PhoneNumberController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetPhoneNumbersQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}
