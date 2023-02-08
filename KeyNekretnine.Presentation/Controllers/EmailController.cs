using Application.Commands.EmailCommands;
using KeyNekretnine.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [ServiceFilter(typeof(BanUserChack))]
        [HttpPost]
        [Route("user/confirm")]
        public async Task<IActionResult> SendEmailConfirm()
        {
            var email = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Email).Value;

            await _sender.Send(new SendConfirmEmailCommand(email));

            return Ok();
        }
    }
}
