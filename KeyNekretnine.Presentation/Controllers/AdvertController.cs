using Application.Commands.AdvertCommands;
using Application.Queries.AdvertQueries;
using Application.Queries.AdvertQuery;
using KeyNekretnine.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdvertController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;

    public AdvertController(ISender sender, IPublisher publisher)
    {
        _sender = sender;
        _publisher = publisher;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _sender.Send(new GetAdvertQuery { Id = id, BypassCache = false }));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaging([FromQuery] AdvertParameters advertParameters)
    {
        return Ok(await _sender.Send(new GetAdvertsQuery(advertParameters)));
    }

    [HttpGet("map")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMapPoints()
    {
        return Ok(await _sender.Send(new GetMapPointsQuery()));
    }

    [HttpGet("map/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdvertFromMapPoint(int id)
    {
        return Ok(await _sender.Send(new GetAdvertFromMapQuery(id)));
    }

    [Authorize]
    [HttpPost]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] AddAdvertDto newAdvert)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        await _sender.Send(new CreateAdvertCommand(newAdvert, email));

        return Accepted();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}/approve")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Approve(int id)
    {

        await _publisher.Publish(new ApproveAdvertCommand(id));

        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}/decline")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Decline(int id)
    {
        await _publisher.Publish(new DeclineAdvertCommand(id));

        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("/api/admin/advert")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdminAdverts([FromQuery] AdminAdvertParameters adminAdvertParameters)
    {
        return Ok(await _sender.Send(new GetAdminAdvertsQuery(adminAdvertParameters)));
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("/api/admin/advert/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdminAdvert(int id)
    {
        return Ok(await _sender.Send(new GetAdminAdvertQuery(id)));
    }

    [Authorize]
    [HttpGet("/api/advert/my-adverts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyAdverts([FromQuery] AdvertParameters advertParameters)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        return Ok(await _sender.Send(new GetMyAdvertsQuery(advertParameters, email)));
    }
}