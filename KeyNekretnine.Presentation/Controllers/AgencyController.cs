//using KeyNekretnine.Presentation.ActionFilters;
//using KeyNekretnine.Presentation.Infrastructure;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Shared.DataTransferObjects.Agency;
//using System.Security.Claims;

//namespace KeyNekretnine.Presentation.Controllers;

//[Route("api/[controller]")]
//public class AgencyController : ApiController
//{

//    public AgencyController(ISender sender)
//        : base(sender)
//    {
//    }


//    [HttpPost]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> CreateAgency([FromBody] CreateAgencyDto createAgencyDto, CancellationToken cancellationToken)
//    {
//        var command = new CreateAgencyCommand(createAgencyDto);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? NoContent() : HandleFailure(response);
//    }

//    [Authorize]
//    [ServiceFilter(typeof(BanUserChack))]
//    [HttpPut("{agencyId}")]
//    [ProducesResponseType(StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> UpdateAgency([FromForm] UpdateAgencyDto updateAgencyDto, int agencyId, CancellationToken cancellationToken)
//    {
//        var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

//        var command = new UpdateAgencyCommand(updateAgencyDto, agencyId, email);

//        var response = await Sender.Send(command, cancellationToken);

//        return response.IsSuccess ? Accepted() : HandleFailure(response);
//    }
//}