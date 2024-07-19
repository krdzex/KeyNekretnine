using KeyNekretnine.Application.Core.Adverts.Commands.ActivateAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.ApproveBasicUpdate;
using KeyNekretnine.Application.Core.Adverts.Commands.ApproveFeaturesUpdate;
using KeyNekretnine.Application.Core.Adverts.Commands.ApproveLocationUpdate;
using KeyNekretnine.Application.Core.Adverts.Commands.CreateAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertFavorite;
using KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertPremium;
using KeyNekretnine.Application.Core.Adverts.Commands.PauseAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.RejectAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.RejectBasicUpdate;
using KeyNekretnine.Application.Core.Adverts.Commands.RejectFeaturesUpdate;
using KeyNekretnine.Application.Core.Adverts.Commands.RejectLocationUpdate;
using KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertFromFavorite;
using KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertPremium;
using KeyNekretnine.Application.Core.Adverts.Commands.ReportAdvert;
using KeyNekretnine.Application.Core.Adverts.Commands.SendEmailToOwner;
using KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertBasic;
using KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertFeatures;
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
using KeyNekretnine.Application.Core.Adverts.Queries.GetFeaturesUpdate;
using KeyNekretnine.Application.Core.Adverts.Queries.GetFilteredAdvertCoordinates;
using KeyNekretnine.Application.Core.Adverts.Queries.GetIsAdvertFavorite;
using KeyNekretnine.Application.Core.Adverts.Queries.GetLocationUpdate;
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

    /// <summary>
    /// You can search for Advert here using Reference Id.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{referenceId}")]
    public async Task<IActionResult> GetAdvertByReferenceId(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertByReferenceIdQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    /// <summary>
    /// Get coordinates of all approved adverts
    /// </summary>
    [AllowAnonymous]
    [HttpGet("map/coordinates")]
    public async Task<IActionResult> GetAllAdvertCoordinates(CancellationToken cancellationToken)
    {
        var query = new GetAllAdvertCoordinatesQuery();

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Retrieves an advertisement corresponding to a map point by its reference ID.
    /// </summary>
    [HttpGet("map/{referenceId}")]
    public async Task<IActionResult> GetAdvertFromMapPoint(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertFromMapByReferenceIdQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    /// <summary>
    /// Retrieves a specific advertisement for admin by its reference ID.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpGet("/api/admin/advert/{referenceId}")]
    public async Task<IActionResult> GetAdvertForAdmin(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertForAdminByReferenceIdQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    /// <summary>
    /// Retrieves a paginated list of advertisements for admin.
    /// </summary>
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

    /// <summary>
    /// Retrieves a paginated list of advertisement reports for admin.
    /// </summary>
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

    /// <summary>
    /// Retrieves a paginated list of advertisements owned by the currently logged-in user.
    /// </summary>
    [Authorize]
    [HttpGet("/api/advert/my-adverts")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> GetMyAdverts([FromQuery] MyAdvertsPaginationParameters request, CancellationToken cancellationToken)
    {
        var query = new GetPagedMyAdvertsQuery(
            request.OrderBy,
            request.PageNumber,
            request.PageSize,
            request.Type,
            request.Purpose,
            request.Status);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Checks if the specified advertisement is marked as a favorite by the currently logged-in user.
    /// </summary>
    [Authorize]
    [HttpGet("{referenceId}/is-favorite")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> GetIsAdvertFavorite(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetIsAdvertFavoriteQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Retrieves a paginated list of favorite advertisements.
    /// </summary>
    [Authorize]
    [HttpGet("favorite")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> GetFavoriteAdverts([FromQuery] FavoriteAdvertsPaginationParameters request, CancellationToken cancellationToken)
    {
        var query = new GetFavoriteAdvertsQuery(
            request.OrderBy,
            request.PageNumber,
            request.PageSize);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Retrieves an advertisement belonging to the currently logged-in user by its reference ID.
    /// </summary>
    [Authorize]
    [HttpGet("/api/advert/my-adverts/{referenceId}")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> GetMyAdvertByReferenceId(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetMyAdvertByReferenceIdQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    /// <summary>
    /// Compares two advertisements by their reference IDs.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("compare/{firstReferenceId}/{secondReferenceId}")]
    public async Task<IActionResult> GetAdvertsCompare(string firstReferenceId, string secondReferenceId, CancellationToken cancellationToken)
    {
        var query = new GetAdvertsCompareQuery(firstReferenceId, secondReferenceId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Retrieves a paginated list of advertisements based on specified filtering and pagination parameters.
    /// </summary>
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

    /// <summary>
    /// Retrieves coordinates of filtered advertisements on a map based on specified filtering parameters.
    /// </summary>
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
            request.Neighborhoods,
            request.Type,
            request.Purpose,
            request.CitySlug,
            request.IsEmergency,
            request.IsUnderConstruction,
            request.IsFurnished);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Approves an advertisement by its reference ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPut("{referenceId}/approve")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> ApproveAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var command = new ApproveAdvertCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Reject an advertisement by its reference ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPut("{referenceId}/reject")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> RejectAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var command = new RejectAdvertCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Reports an advertisement by its reference ID.
    /// </summary>
    [Authorize]
    [HttpPost("{referenceId}/report")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> ReportAdvert(string referenceId, [FromBody] ReportAdvertRequest request, CancellationToken cancellationToken)
    {
        var command = new ReportAdvertCommand(referenceId, request.RejectReasonId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);

    }

    /// <summary>
    /// Marks an advertisement as favorite by its reference ID for the currently logged-in user.
    /// </summary>
    [Authorize]
    [HttpPost("{referenceId}/favorite")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> MakeAdvertToFavorite(string referenceId, CancellationToken cancellationToken)
    {
        var command = new MakeAdvertFavoriteCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Removes an advertisement from the favorites of the currently logged-in user by its reference ID.
    /// </summary>
    [Authorize]
    [HttpDelete("{referenceId}/favorite")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> RemoveAdvertFromFavorite(string referenceId, CancellationToken cancellationToken)
    {
        var command = new RemoveAdvertFromFavoriteCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Upgrades an advertisement to premium status by its reference ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPut("{referenceId}/make-premium")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> MakeAdvertPremium(string referenceId, CancellationToken cancellationToken)
    {
        var command = new MakeAdvertPremiumCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Removes premium status from an advertisement by its reference ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPut("{referenceId}/remove-premium")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> RemoveAdvertPremium(string referenceId, CancellationToken cancellationToken)
    {
        var command = new RemoveAdvertPremiumCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Pauses an advertisement by its reference ID.
    /// </summary>
    [Authorize]
    [HttpPut("{referenceId}/pause")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> PauseAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var command = new PauseAdvertCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Activates an advertisement by its reference ID.
    /// </summary>
    [Authorize]
    [HttpPut("{referenceId}/activate")]
    //[ServiceFilter(typeof(BanUserChack))]
    public async Task<IActionResult> ActivateAdvert(string referenceId, CancellationToken cancellationToken)
    {
        var command = new ActivateAdvertCommand(referenceId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Retrieves recommended advertisements based on a reference ID.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{referenceId}/recommended")]
    public async Task<IActionResult> GetRecommendedAdverts(string referenceId, CancellationToken cancellationToken)
    {
        var query = new GetRecommendedAdvertsQuery(referenceId);

        var response = await _sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }

    /// <summary>
    /// Sends an email message to the owner of a specific advertisement.
    /// </summary>
    [AllowAnonymous]
    [EnableRateLimiting("low-rating")]
    [HttpPost("{referenceId}/send-email")]
    public async Task<IActionResult> SendMessageToOwner([FromBody] SendMessageToOwnerRequest request, string referenceId, CancellationToken cancellationToken)
    {
        var command = new SendEmailToOwnerCommand(referenceId, request.FullName, request.PhoneNumber, request.SenderEmail, request.Message);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }


    /// <summary>
    /// Updates the basic information of an advertisement by its reference ID.
    /// </summary>
    [Authorize]
    [HttpPut("{referenceId}/basic-informations")]
    public async Task<IActionResult> UpdateBasicInformations([FromBody] AdvertBasicUpdateRequest request, string referenceId, CancellationToken cancellationToken)
    {
        var command = new UpdateAdvertBasicCommand(referenceId, request);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Updates the location of an advertisement by its reference ID.
    /// </summary>
    [Authorize]
    [HttpPut("{referenceId}/location")]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateAdvertLocationRequest request, string referenceId, CancellationToken cancellationToken)
    {
        var command = new UpdateAdvertLocationCommand(referenceId, request);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Updates the features of an advertisement by its reference ID.
    /// </summary>
    [Authorize]
    [HttpPut("{referenceId}/features")]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateAdvertFeaturesRequest request, string referenceId, CancellationToken cancellationToken)
    {
        var command = new UpdateAdvertFeaturesCommand(referenceId, request);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Retrieves updates for advertisements for admin.
    /// </summary>
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

    /// <summary>
    /// Retrieves a basic update for an advertisement by its update ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpGet("basic-update/{updateId}")]
    public async Task<IActionResult> GetBasicUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var query = new GetBasicUpdateQuery(updateId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    /// <summary>
    /// Retrieves a location for an advertisement by its update ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpGet("location/{updateId}")]
    public async Task<IActionResult> GetLocationUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var query = new GetLocationUpdateQuery(updateId);

        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    /// <summary>
    /// Retrieves a features update for an advertisement by its update ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpGet("features/{updateId}")]
    public async Task<IActionResult> GetFeaturesUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var query = new GetFeaturesUpdateQuery(updateId);

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

    /// <summary>
    /// Approves a basic update for an advertisement by its update ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPut("features/{updateId}/approve")]
    public async Task<IActionResult> ApproveFeaturesUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var command = new ApproveFeaturesUpdateCommand(updateId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Approves a location update for an advertisement by its update ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPut("location/{updateId}/approve")]
    public async Task<IActionResult> ApproveLocationUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var command = new ApproveLocationUpdateCommand(updateId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Reject a basic update for an advertisement by its update ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPut("basic-update/{updateId}/reject")]
    public async Task<IActionResult> RejectBasicUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var command = new RejectBasicUpdateCommand(updateId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Reject a location update for an advertisement by its update ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPut("location/{updateId}/reject")]
    public async Task<IActionResult> RejectLocationUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var command = new RejectLocationUpdateCommand(updateId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Reject a features update for an advertisement by its update ID for admin.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPut("features/{updateId}/reject")]
    public async Task<IActionResult> RejectFeaturesUpdate(Guid updateId, CancellationToken cancellationToken)
    {
        var command = new RejectFeaturesUpdateCommand(updateId);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }

    /// <summary>
    /// Uploads an image for an advertisement.
    /// </summary>
    [Authorize]
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile image, CancellationToken cancellationToken)
    {
        var command = new CreateAdvertImageCommand(image);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    /// <summary>
    /// Create an advertisement.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAdvertRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAdvertCommand(request);

        var response = await _sender.Send(command, cancellationToken);

        return response.IsSuccess ? NoContent() : BadRequest(response.Error);
    }
}