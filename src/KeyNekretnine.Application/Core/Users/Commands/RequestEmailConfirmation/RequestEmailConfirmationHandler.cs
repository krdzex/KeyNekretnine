using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace KeyNekretnine.Application.Core.Users.Commands.RequestEmailConfirmation;

internal sealed class RequestEmailConfirmationHandler : ICommandHandler<RequestEmailConfirmationCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IUserContext _userContext;

    public RequestEmailConfirmationHandler(
        UserManager<User> userManager,
        IEmailService emailService,
        IUserContext userContext)
    {
        _userManager = userManager;
        _emailService = emailService;
        _userContext = userContext;
    }

    public async Task<Result> Handle(RequestEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_userContext.UserId);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var encodedToken = HttpUtility.UrlEncode(token);

        var sendEmailResult = await _emailService.SendEmailConfrim(user.Email, encodedToken, cancellationToken);

        if (!sendEmailResult)
        {
            return Result.Failure(new Error("Email.Error", "There was error with email service"));
        }

        return Result.Success();
    }
}