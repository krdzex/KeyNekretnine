using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Exceptions;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace KeyNekretnine.Application.Core.Users.Commands.PasswordForgot;
internal sealed class PasswordForgotHandler : ICommandHandler<PasswordForgotCommand>
{
    private readonly UserManager<User> _userManager;

    public PasswordForgotHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(PasswordForgotCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        var decodedToken = HttpUtility.UrlDecode(request.Token);

        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

        if (!resetPasswordResult.Succeeded)
        {
            var errors = resetPasswordResult.Errors
            .Select(authenticationFailure => new AuthenticationError(
                authenticationFailure.Code,
                authenticationFailure.Description))
            .ToList();

            throw new AuthenticationException(errors);
        }

        return Result.Success();
    }
}