using Application.Commands.AdvertCommands;
using Application.Notifications.AdvertNotifications;
using Application.Queries.AdvertQueries;
using Application.Queries.AdvertQuery;
using KeyNekretnine.Attributes;
using KeyNekretnine.Presentation.ActionFilters;
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
        return Ok(await _sender.Send(new GetAdvertQuery { Id = id, BypassCache = true }));
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
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Approve(int id)
    {
        await _publisher.Publish(new ApproveAdvertNotification(id));

        return NoContent();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}/decline")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Decline(int id)
    {
        await _publisher.Publish(new DeclineAdvertNotification(id));

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
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyAdverts([FromQuery] MyAdvertsParameters myAdvertParameters)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        return Ok(await _sender.Send(new GetMyAdvertsQuery(myAdvertParameters, email)));
    }

    [Authorize]
    [HttpGet("/api/advert/my-adverts/{advertId}")]
    [ServiceFilter(typeof(BanUserChack))]
    [ServiceFilter(typeof(OwnerAdvertChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyAdverts(int advertId)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        return Ok(await _sender.Send(new GetMyAdvertQuery(advertId, email)));
    }

    [Authorize]
    [HttpPost("{advertId}/favorite")]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MakeAdvertToFavorite(int advertId)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        await _sender.Send(new MakeAdvertFavoriteCommand(advertId, email));

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{advertId}/favorite")]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveAdvertFromFavorite(int advertId)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        await _sender.Send(new RemoveAdvertFromFavoriteCommand(advertId, email));

        return NoContent();
    }

    [Authorize]
    [HttpGet("favorite")]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FavoriteAdverts([FromQuery] FavoriteAdvertsParameters requestParameters)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        return Ok(await _sender.Send(new GetFavoriteAdvertsQuery(requestParameters, email)));
    }

    [Authorize]
    [HttpGet("{advertId}/is-favorite")]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> IsAdvertFavorite(int advertId)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        return Ok(await _sender.Send(new GetIsFavoriteAdvertQuery(advertId, email)));
    }


    [Authorize]
    [HttpPost("{advertId}/report")]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReportAdvert(int advertId, [FromBody] ReportAdvertDto reportAdvertDto)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        await _sender.Send(new ReportAdvertCommand(advertId, email, reportAdvertDto.RejectReasonId));

        return NoContent();
    }

    [HttpGet("report")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdvertReports([FromQuery] ReportParameters reportParameters)
    {
        return Ok(await _sender.Send(new GetAdvertReportsQuery(reportParameters)));
    }


    [HttpGet("compare/{firstAdvert}/{sacondAdvert}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdvertsCompare(int firstAdvert, int sacondAdvert)
    {
        return Ok(await _sender.Send(new GetAdvertsCompareQuery(firstAdvert, sacondAdvert)));
    }

    [HttpPut("{advertId}/update/informations")]
    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [ServiceFilter(typeof(OwnerAdvertChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateInformations([FromBody] UpdateAdvertInformationsDto updateAdvertInformationsDto, int advertId)
    {
        await _sender.Send(new UpdateAdvertCommand(updateAdvertInformationsDto, advertId));

        return NoContent();
    }

    [HttpPut("{advertId}/update/location")]
    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [ServiceFilter(typeof(OwnerAdvertChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateAdvertLocationDto updateAdvertLocationDto, int advertId)
    {
        await _sender.Send(new UpdateAdvertLocationCommand(updateAdvertLocationDto, advertId));

        return NoContent();
    }
}