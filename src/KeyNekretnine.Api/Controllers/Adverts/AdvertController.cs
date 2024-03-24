using KeyNekretnine.Application.Core.Adverts.Commands.ActivateAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.ApproveBasicUpdate;
using KeyNekretnine.Application.Core.Adverts.Commands.CreateAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertFavorite;
using KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertPremium;
using KeyNekretnine.Application.Core.Adverts.Commands.PauseAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertFromFavorite;
using KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertPremium;
using KeyNekretnine.Application.Core.Adverts.Commands.ReportAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.SendEmailToOwner;
using KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertBasic;
using KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
using KeyNekretnine.Application.Core.Adverts.Commands.UploadAdvertImage;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertForAdminByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertFromMapByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertsCompare;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAdvertUpdates;
using KeyNekretnine.Application.Core.Adverts.Queries.GetAllAdvertCoordinates;
using KeyNekretnine.Application.Core.Adverts.Queries.GetBasicUpdate;
using KeyNekretnine.Application.Core.Adverts.Queries.GetFavoriteAdverts;
using KeyNekretnine.Application.Core.Adverts.Queries.GetFilteredAdvertCoordinates;
using KeyNekretnine.Application.Core.Adverts.Queries.GetIsAdvertFavorite;
using KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdvertByReferenceId;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertReportsForAdmin;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdverts;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedAdvertsForAdmin;
using KeyNekretnine.Application.Core.Adverts.Queries.GetPagedMyAdverts;
using KeyNekretnine.Application.Core.Adverts.Queries.GetRecommendedAdverts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace KeyNekretnine.Api.Controllers.Adverts;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("high-rating")]
public class AdvertController : ControllerBase
{
    private readonly ISender _sender;
    public AdvertController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpGet("{referenceId}")]
    public async Task<IActionResult> GetAdvertByReferenceId(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertByReferenceIdQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpGet("compare/{firstReferenceId}/{secondReferenceId}")]
    public async Task<IActionResult> GetAdvertsCompare(string firstReferenceId, string secondReferenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertsCompareQuery(firstReferenceId, secondReferenceId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [AllowAnonymous]
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
            request.Type,
            request.Purpose,
            request.Neighborhoods,
            request.CitySlug,
            request.IsEmergency,
            request.IsUnderConstruction,
            request.IsFurnished);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [AllowAnonymous]
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
            request.CitySlug,
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

    [Authorize]
    [HttpPut("{referenceId}/pause")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> PauseAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;
        var isAgency = bool.Parse(User.Claims.FirstOrDefault(q => q.Type == "isAgency").Value);

        var command = new PauseAdvertCommand(referenceId, userId, isAgency);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [Authorize]
    [HttpPut("{referenceId}/activate")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> ActivateAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var userId = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier).Value;
        var isAgency = bool.Parse(User.Claims.FirstOrDefault(q => q.Type == "isAgency").Value);

        var command = new ActivateAdvertCommand(referenceId, userId, isAgency);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [AllowAnonymous]
    [HttpGet("{referenceId}/recommended")]
    public async Task<IActionResult> GetRecommendedAdverts(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetRecommendedAdvertsQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    [AllowAnonymous]
    [EnableRateLimiting("low-rating")]
    [HttpPost("{referenceId}/send-email")]
    public async Task<IActionResult> SendMessageToOwner([FromBody] SendMessageToOwnerRequest request, string referenceId, CancellationToken cancellationToken)
    {
        var command = new SendEmailToOwnerCommand(referenceId, request.FullName, request.PhoneNumber, request.SenderEmail, request.Message);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }


    [Authorize]
    [HttpPut("{referenceId}/basic-informations")]
    public async Task<IActionResult> UpdateBasicInformations([FromBody] AdvertBasicUpdateRequest request, string referenceId, CancellationToken cancellationToken)
    {
        var command = new UpdateAdvertBasicCommand(referenceId, request);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("updates")]
    public async Task<IActionResult> GetAdvertUpdates([FromQuery] AdvertUpdatesPaginationParameters request, string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertUpdatesQuery(
            request.OrderBy,
            request.PageNumber,
            request.PageSize,
            request.UpdateType,
            request.ReferenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("basic-update/{updateId}")]
    public async Task<IActionResult> GetBasicUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var query = new GetBasicUpdateQuery(updateId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("basic-update/{updateId}/approve")]
    public async Task<IActionResult> ApproveBasicUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var command = new ApproveBasicUpdateCommand(updateId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    [Authorize]
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile image, CancellationToken cancellationToken)
    {
        var command = new CreateAdvertImageCommand(image);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAdvertRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAdvertCommand(request);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }
}