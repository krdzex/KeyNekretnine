//using KeyNekretnine.Presentation.ActionFilters;
//using KeyNekretnine.Presentation.Infrastructure;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Shared.DataTransferObjects.Advert;
//using Shared.RequestFeatures;
//using System.Security.Claims;

//namespace KeyNekretnine.Presentation.Controllers;

//[Route("api/[controller]")]
//public class AdvertController : ApiController
//{
//    public AdvertController(ISender sender)
//        : base(sender)
//    { }

//    [HttpGet("{id}")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
//    {
//        var query = new GetAdvertByIdQuery(id);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [HttpGet]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> Get([FromQuery] AdvertParameters advertParameters, CancellationToken cancellationToken)
//    {
//        var query = new GetAdvertsQuery(advertParameters);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [HttpGet("map")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> GetMapPoints(CancellationToken cancellationToken)
//    {
//        var query = new GetMapPointsQuery();

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [HttpGet("map/{id}")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> GetAdvertFromMapPoint(int id, CancellationToken cancellationToken)
//    {
//        var query = new GetAdvertFromMapQuery(id);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [Authorize]
//    [HttpPost]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ProducesResponseType(StatusCodes.Status202Accepted)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> Create([FromForm] AddAdvertDto newAdvert, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var command = new CreateAdvertCommand(newAdvert, email);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? Accepted() : HandleFailure(response);
//    }

//    [Authorize(Roles = "Administrator")]
//    [HttpPut("{id}/approve")]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken)
//    {
//        var command = new ApproveAdvertCommand(id);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);
//    }

//    [Authorize(Roles = "Administrator")]
//    [HttpPut("{id}/decline")]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> Decline(int id, CancellationToken cancellationToken)
//    {
//        var command = new RejectAdvertCommand(id);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);
//    }

//    [Authorize(Roles = "Administrator")]
//    [HttpGet("/api/admin/advert")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> GetAdminAdverts([FromQuery] AdminAdvertParameters adminAdvertParameters, CancellationToken cancellationToken)
//    {
//        var query = new GetAdminAdvertsQuery(adminAdvertParameters);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [Authorize(Roles = "Administrator")]
//    [HttpGet("/api/admin/advert/{id}")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> GetAdminAdvert(int id, CancellationToken cancellationToken)
//    {
//        var query = new GetAdminAdvertByIdQuery(id);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [Authorize]
//    [HttpGet("/api/advert/my-adverts")]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> GetMyAdverts([FromQuery] MyAdvertsParameters myAdvertParameters, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var query = new GetMyAdvertsQuery(myAdvertParameters, email);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [Authorize]
//    [HttpGet("/api/advert/my-adverts/{advertId}")]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ServiceFilter(typeof(OwnerAdvertChack))]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

//    public async Task<IActionResult> GetMyAdverts(int advertId, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var query = new GetMyAdvertByIdQuery(advertId, email);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [Authorize]
//    [HttpPost("{advertId}/favorite")]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> MakeAdvertToFavorite(int advertId, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var command = new MakeAdvertFavoriteCommand(advertId, email);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);
//    }

//    [Authorize]
//    [HttpDelete("{advertId}/favorite")]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> RemoveAdvertFromFavorite(int advertId, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var command = new RemoveAdvertFromFavoriteCommand(advertId, email);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);
//    }

//    [Authorize]
//    [HttpGet("favorite")]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> FavoriteAdverts([FromQuery] FavoriteAdvertsParameters requestParameters, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var query = new GetFavoriteAdvertsQuery(requestParameters, email);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [Authorize]
//    [HttpGet("{advertId}/is-favorite")]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> IsAdvertFavorite(int advertId, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var query = new GetIsAdvertFavoriteQuery(advertId, email);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }


//    [Authorize]
//    [HttpPost("{advertId}/report")]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> ReportAdvert(int advertId, [FromBody] ReportAdvertDto reportAdvertDto, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var command = new ReportAdvertCommand(advertId, email, reportAdvertDto.RejectReasonId);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);

//    }

//    [HttpGet("report")]
//    [Authorize(Roles = "Administrator")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> GetAdvertReports([FromQuery] ReportParameters reportParameters, CancellationToken cancellationToken)
//    {
//        var query = new GetAdvertReportsQuery(reportParameters);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }


//    [HttpGet("compare/{firstAdvert}/{sacondAdvert}")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> GetAdvertsCompare(int firstAdvert, int sacondAdvert, CancellationToken cancellationToken)
//    {
//        var query = new GetAdvertsCompareQuery(firstAdvert, sacondAdvert);

//        var response = await Sender.Send(query, cancellationToken);

//        return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
//    }

//    [HttpPut("{advertId}/update/informations")]
//    [Authorize]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ServiceFilter(typeof(OwnerAdvertChack))]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> UpdateInformations([FromBody] UpdateAdvertInformationsDto updateAdvertInformationsDto, int advertId, CancellationToken cancellationToken)
//    {
//        var command = new UpdateAdvertCommand(updateAdvertInformationsDto, advertId);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);
//    }

//    [HttpPut("{advertId}/update/location")]
//    [Authorize]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ServiceFilter(typeof(OwnerAdvertChack))]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    public async Task<IActionResult> UpdateLocation([FromBody] UpdateAdvertLocationDto updateAdvertLocationDto, int advertId, CancellationToken cancellationToken)
//    {
//        var command = new UpdateAdvertLocationCommand(updateAdvertLocationDto, advertId);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);
//    }

//    [HttpDelete("{advertId}/images")]
//    [Authorize]
//    [ServiceFilter(typeof(BanUserChack))]
//    [ServiceFilter(typeof(OwnerAdvertChack))]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

//    public async Task<IActionResult> DeleteImages([FromBody] IEnumerable<string> imageUrls, int advertId, CancellationToken cancellationToken)
//    {
//        var command = new DeleteImagesCommand(imageUrls, advertId);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);
//    }
//}