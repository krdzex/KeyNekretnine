using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Exceptions;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Shared;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserRegistration;
internal sealed class UserRegistrationHandler : ICommandHandler<UserRegistrationCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IDateTimeProvider _dateTimeProvider;
    public UserRegistrationHandler(UserManager<User> userManager, IDateTimeProvider dateTimeProvider)
    {
        _userManager = userManager;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
           new FirstName(request.FirstName),
           new LastName(request.LastName),
           request.Email,
           request.UserName,
           _dateTimeProvider.Now,
           false);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors
            .Select(authenticationFailure => new AuthenticationError(
                authenticationFailure.Code,
                authenticationFailure.Description))
            .ToList();

            throw new AuthenticationException(errors);
        }

        var addRoleRsult = await _userManager.AddToRoleAsync(user, "User");

        if (!addRoleRsult.Succeeded)
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