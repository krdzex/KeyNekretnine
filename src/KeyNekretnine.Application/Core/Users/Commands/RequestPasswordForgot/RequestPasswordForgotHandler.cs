using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace KeyNekretnine.Application.Core.Users.Commands.RequestPasswordForgot;
internal sealed class RequestPasswordForgotHandler : ICommandHandler<RequestPasswordForgotCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public RequestPasswordForgotHandler(
        UserManager<User> userManager,
        IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<Result> Handle(RequestPasswordForgotCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null && user.PasswordHash is not null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = HttpUtility.UrlEncode(token);

            var sendEmailResult = await _emailService.SendResetPasswordLink(user.Email, encodedToken, cancellationToken);

            if (!sendEmailResult)
            {
                return Result.Failure(new Error("Email.Error", "There was error with email service"));
            }
        }

        return Result.Success();
    }
}