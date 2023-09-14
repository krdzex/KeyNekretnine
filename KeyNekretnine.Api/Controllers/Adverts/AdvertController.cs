using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertForAdminByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertFromMapByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAllAdvertCoordinates;
using KeyNekretnine.Application.Core.Adverts.Queries.GetFavoriteAdverts;
using KeyNekretnine.Application.Core.Adverts.Queries.GetIsAdvertFavorite;
using KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdverts;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertReportsForAdmin;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertsForAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KeyNekretnine.Api.Controllers.Adverts;

[ApiController]
[Route("api/[controller]")]
public class AdvertController : ControllerBase
{
    private readonly ISender _sender;
    public AdvertController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{referenceId}")]
    public async Task<IActionResult> Get(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertByReferenceIdQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet("/map/coordinates")]
    public async Task<IActionResult> GetAllAdvertCoordinates(CancellationToken cancellationToken)
    {
        var query = new GetAllAdvertCoordinatesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("map/{referenceId}")]
    public async Task<IActionResult> GetAdvertFromMapPoint(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertFromMapByReferenceIdQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("/api/admin/advert/{referenceId}")]
    public async Task<IActionResult> GetAdvertForAdmin(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertForAdminByReferenceIdQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("/api/admin/advert")]
    public async Task<IActionResult> GetAdminAdverts([FromQuery] AdminAdvertPaginationParameters request, CancellationToken cancellationToken)
    {
        var query = new GetPagedAdvertsForAdminQuery(
            request.OrderBy,
            request.PageNumber,
            request.PageSize,
            request.ReferenceId,
            request.Type,
            request.Purpose,
            request.Status);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("report")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetAdvertReports([FromQuery] AdvertReportsPaginationParameters request, CancellationToken cancellationToken)
    {
        var query = new GetPagedAdvertReportsForAdminQuery(
            request.OrderBy,
            request.PageNumber,
            request.PageSize);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [Authorize]
    [HttpGet("/api/advert/my-adverts")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> GetMyAdverts([FromQuery] MyAdvertsPaginationParameters request, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var query = new GetPagedMyAdvertsQuery(
            request.OrderBy,
            request.PageNumber,
            request.PageSize,
            userId,
            request.Type,
            request.Purpose,
            request.Status);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [Authorize]
    [HttpGet("{referenceId}/is-favorite")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> GetIsAdvertFavorite(string referenceId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var query = new GetIsAdvertFavoriteQuery(referenceId, userId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [Authorize]
    [HttpGet("favorite")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> FavoriteAdverts([FromQuery] FavoriteAdvertsPaginationParameters request, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var query = new GetFavoriteAdvertsQuery(
            request.OrderBy,
            request.PageNumber,
            request.PageSize,
            userId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
}