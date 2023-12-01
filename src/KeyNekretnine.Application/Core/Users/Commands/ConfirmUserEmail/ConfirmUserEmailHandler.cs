using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Exceptions;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Users.Commands.ConfirmUserEmail;
internal sealed class ConfirmUserEmailHandler : ICommandHandler<ConfirmUserEmailCommand>
{
    private readonly UserManager<User> _userManager;

    public ConfirmUserEmailHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure<bool>(UserErrors.NotFound);
        }

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (!result.Succeeded)
        {
            var errors = result.Errors
                .Select(authenticationFailure => new AuthenticationError(
                    authenticationFailure.Code,
                    authenticationFailure.Description))
                .ToList();

            throw new AuthenticationException(errors);
        }

        return Result.Success();
    }
}