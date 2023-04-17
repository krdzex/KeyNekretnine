using Application.Commands.EmailCommands;
using KeyNekretnine.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KeyNekretnine.Presentation.Controllers
{
    public class EmailController : ControllerBase
    {
        private readonly ISender _sender;

        public EmailController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Authorize]
        [Route("/api/user/confirm")]
        [ServiceFilter(typeof(BanUserChack))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEmailConfirm()
        {
            var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

            await _sender.Send(new SendConfirmEmailCommand(email));

            return Ok();
        }
    }
}
