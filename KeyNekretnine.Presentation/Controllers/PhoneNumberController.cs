﻿using Application.Core.PhoneNumbers.Queries.GetPhoneNumbers;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/phone-number")]
public class PhoneNumberController : ApiController
{
    public PhoneNumberController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetPhoneNumbersQuery();

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }
}
