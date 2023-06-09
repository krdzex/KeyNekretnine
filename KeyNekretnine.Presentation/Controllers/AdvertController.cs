using Application.Adverts.Queries.GetAdvertById;
using Application.Commands.AdvertCommands;
using Application.Core.Adverts.Queries.GetAdminAdvert;
using Application.Core.Adverts.Queries.GetAdminAdverts;
using Application.Core.Adverts.Queries.GetAdvertFromMap;
using Application.Core.Adverts.Queries.GetAdvertReports;
using Application.Core.Adverts.Queries.GetAdverts;
using Application.Core.Adverts.Queries.GetADvertsCompare;
using Application.Core.Adverts.Queries.GetFavoriteAdverts;
using Application.Core.Adverts.Queries.GetIsFavorite;
using Application.Core.Adverts.Queries.GetmapPoints;
using Application.Core.Adverts.Queries.GetMyAdvertById;
using Application.Core.Adverts.Queries.GetMyAdverts;
using Application.Notifications.AdvertNotifications;
using KeyNekretnine.Attributes;
using KeyNekretnine.Presentation.ActionFilters;
using KeyNekretnine.Presentation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.Controllers;

[Route("api/[controller]")]
public class AdvertController : ApiController
{
    private readonly IPublisher _publisher;

    public AdvertController(ISender sender, IPublisher publisher)
        : base(sender)
    {
        _publisher = publisher;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var query = new GetAdvertByIdQuery(id);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] AdvertParameters advertParameters, CancellationToken cancellationToken)
    {
        var query = new GetAdvertsQuery(advertParameters);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [HttpGet("map")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMapPoints(CancellationToken cancellationToken)
    {
        var query = new GetMapPointsQuery();

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [HttpGet("map/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdvertFromMapPoint(int id, CancellationToken cancellationToken)
    {
        var query = new GetAdvertFromMapQuery(id);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
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

        await Sender.Send(new CreateAdvertCommand(newAdvert, email));

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
    public async Task<IActionResult> GetAdminAdverts([FromQuery] AdminAdvertParameters adminAdvertParameters, CancellationToken cancellationToken)
    {
        var query = new GetAdminAdvertsQuery(adminAdvertParameters);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("/api/admin/advert/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdminAdvert(int id, CancellationToken cancellationToken)
    {
        var query = new GetAdminAdvertByIdQuery(id);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [Authorize]
    [HttpGet("/api/advert/my-adverts")]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyAdverts([FromQuery] MyAdvertsParameters myAdvertParameters, CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var query = new GetMyAdvertsQuery(myAdvertParameters, email);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [Authorize]
    [HttpGet("/api/advert/my-adverts/{advertId}")]
    [ServiceFilter(typeof(BanUserChack))]
    [ServiceFilter(typeof(OwnerAdvertChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyAdverts(int advertId, CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var query = new GetMyAdvertByIdQuery(advertId, email);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
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

        await Sender.Send(new MakeAdvertFavoriteCommand(advertId, email));

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

        await Sender.Send(new RemoveAdvertFromFavoriteCommand(advertId, email));

        return NoContent();
    }

    [Authorize]
    [HttpGet("favorite")]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FavoriteAdverts([FromQuery] FavoriteAdvertsParameters requestParameters, CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var query = new GetFavoriteAdvertsQuery(requestParameters, email);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [Authorize]
    [HttpGet("{advertId}/is-favorite")]
    [ServiceFilter(typeof(BanUserChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> IsAdvertFavorite(int advertId, CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

        var query = new GetIsAdvertFavoriteQuery(advertId, email);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
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

        await Sender.Send(new ReportAdvertCommand(advertId, email, reportAdvertDto.RejectReasonId));

        return NoContent();
    }

    [HttpGet("report")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdvertReports([FromQuery] ReportParameters reportParameters, CancellationToken cancellationToken)
    {
        var query = new GetAdvertReportsQuery(reportParameters);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }


    [HttpGet("compare/{firstAdvert}/{sacondAdvert}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAdvertsCompare(int firstAdvert, int sacondAdvert, CancellationToken cancellationToken)
    {
        var query = new GetAdvertsCompareQuery(firstAdvert, sacondAdvert);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
    }

    [HttpPut("{advertId}/update/informations")]
    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [ServiceFilter(typeof(OwnerAdvertChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateInformations([FromBody] UpdateAdvertInformationsDto updateAdvertInformationsDto, int advertId)
    {
        await Sender.Send(new UpdateAdvertCommand(updateAdvertInformationsDto, advertId));

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
        await Sender.Send(new UpdateAdvertLocationCommand(updateAdvertLocationDto, advertId));

        return NoContent();
    }

    [HttpDelete("{advertId}/images")]
    [Authorize]
    [ServiceFilter(typeof(BanUserChack))]
    [ServiceFilter(typeof(OwnerAdvertChack))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteImages([FromBody] IEnumerable<string> imageUrls, int advertId)
    {
        await Sender.Send(new DeleteImagesCommand(imageUrls, advertId));

        return NoContent();
    }
}