﻿using KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertFavorite;
using KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertPremium;
using KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertFromFavorite;
using KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertPremium;
using KeyNekretnine.Application.Core.Adverts.Commands.ReportAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertForAdminByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertFromMapByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertsCompare;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAllAdvertCoordinates;
using KeyNekretnine.Application.Core.Adverts.Queries.GetFavoriteAdverts;
using KeyNekretnine.Application.Core.Adverts.Queries.GetFilteredAdvertCoordinates;
using KeyNekretnine.Application.Core.Adverts.Queries.GetIsAdvertFavorite;
using KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdvertByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertReportsForAdmin;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdverts;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertsForAdmin;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedMyAdverts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace KeyNekretnine.Api.Controllers.Adverts;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("fixed-by-ip")]
public class AdvertController : ControllerBase
{
    private readonly ISender _sender;
    public AdvertController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{referenceId}")]
    public async Task<IActionResult> GetAdvertByReferenceId(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertByReferenceIdQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet("map/coordinates")]
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
    public async Task<IActionResult> GetFavoriteAdverts([FromQuery] FavoriteAdvertsPaginationParameters request, CancellationToken cancellationToken)
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

    [Authorize]
    [HttpGet("/api/advert/my-adverts/{referenceId}")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> GetMyAdvertByReferenceId(string referenceId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var query = new GetMyAdvertByReferenceIdQuery(referenceId, userId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet("compare/{firstReferenceId}/{secondReferenceId}")]
    public async Task<IActionResult> GetAdvertsCompare(string firstReferenceId, string secondReferenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertsCompareQuery(firstReferenceId, secondReferenceId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] AdvertsPaginationParameters request, CancellationToken cancellationToken)
    {
        var query = new GetPagedAdvertsQuery(
            request.OrderBy,
            request.PageNumber,
            request.PageSize,
            request.MinPrice,
            request.MaxPrice,
            request.MinFloorSpace,
            request.MaxFloorSpace,
            request.NoOfBedrooms,
            request.NoOfBathrooms,
            request.Types,
            request.Purposes,
            request.Neighborhoods,
            request.CityId,
            request.IsEmergency,
            request.IsUnderConstruction,
            request.IsFurnished);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [HttpGet("filtered-coordinates")]
    public async Task<IActionResult> GetFilteredAdvertCoordinates([FromQuery] AdvertMapParameters request, CancellationToken cancellationToken)
    {
        var query = new GetFilteredAdvertCoordinatesQuery(
            request.MinPrice,
            request.MaxPrice,
            request.MinFloorSpace,
            request.MaxFloorSpace,
            request.NoOfBedrooms,
            request.NoOfBathrooms,
            request.Types,
            request.Purposes,
            request.Neighborhoods,
            request.CityId,
            request.IsEmergency,
            request.IsUnderConstruction,
            request.IsFurnished);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{referenceId}/approve")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> ApproveAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var command = new ApproveAdvertCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{referenceId}/reject")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> RejectAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var command = new RejectAdvertCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [HttpPut("{referenceId}/location")]
    [Authorize]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateAdvertLocationRequest request, string referenceId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;
        var isAgency = bool.Parse(User.Claims.FirstOrDefault(q => q.Type == "isAgency").Value);

        var command = new UpdateAdvertLocationCommand(
            referenceId,
            userId,
            request.Latitude,
            request.Longitude,
            request.Address,
            request.NeighborhoodId,
            isAgency);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [Authorize]
    [HttpPost("{referenceId}/report")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> ReportAdvert(string referenceId, [FromBody] ReportAdvertRequest request, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var command = new ReportAdvertCommand(referenceId, userId, request.RejectReasonId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);

    }

    [Authorize]
    [HttpPost("{referenceId}/favorite")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> MakeAdvertToFavorite(string referenceId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var command = new MakeAdvertFavoriteCommand(referenceId, userId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [Authorize]
    [HttpDelete("{referenceId}/favorite")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> RemoveAdvertFromFavorite(string referenceId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;

        var command = new RemoveAdvertFromFavoriteCommand(referenceId, userId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{referenceId}/make-premium")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> MakeAdvertPremium(string referenceId, CancellationToken cancellationToken)
    {
        var command = new MakeAdvertPremiumCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{referenceId}/remove-premium")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> RemoveAdvertPremium(string referenceId, CancellationToken cancellationToken)
    {
        var command = new RemoveAdvertPremiumCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }
}