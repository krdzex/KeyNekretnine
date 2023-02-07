using Application.Commands.EmailCommands;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using System.Web;

namespace Application.Handlers.EmailHandlers;
internal sealed class SendConfirmEmailHandler : IRequestHandler<SendConfirmEmailCommand, Unit>
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;

    public SendConfirmEmailHandler(IServiceManager serviceManager, UserManager<User> userManager)
    {
        _serviceManager = serviceManager;
        _userManager = userManager;
    }
    public async Task<Unit> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        var token = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));

        var result = await _serviceManager.EmailService.SendEmailConfrim(request.Email, token);

        if (!result)
        {
            throw new EmailCouldntBeSentException();
        }

        return Unit.Value;
    }
}

