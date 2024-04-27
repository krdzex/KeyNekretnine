using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Exceptions;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Users.Commands.ChangeUserPassword;
internal sealed class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ChangeUserPasswordHandler(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_userContext.UserId);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        if (user.PasswordHash is null)
        {
            return Result.Failure(UserErrors.CantChangePassword);
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!changePasswordResult.Succeeded)
        {
            var errors = changePasswordResult.Errors
            .Select(authenticationFailure => new AuthenticationError(
                authenticationFailure.Code,
                authenticationFailure.Description))
            .ToList();

            throw new AuthenticationException(errors);
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}