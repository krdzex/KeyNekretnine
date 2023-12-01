using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Application.Exceptions;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Shared;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Auth.Commands.GoogleLogin;
internal sealed class GoogleLoginHandler : ICommandHandler<GoogleLoginCommand, TokenResponse>
{
    private readonly IGoogleService _googleService;
    private readonly UserManager<User> _userManager;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtService _jwtService;

    public GoogleLoginHandler(
        IGoogleService googleService,
        UserManager<User> userManager,
        IDateTimeProvider dateTimeProvider,
        IJwtService jwtService)
    {
        _googleService = googleService;
        _userManager = userManager;
        _dateTimeProvider = dateTimeProvider;
        _jwtService = jwtService;
    }

    public async Task<Result<TokenResponse>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var googleResponse = await _googleService.VerifyGoogleTokenAndGetInformations(request.GoogleIdToken);

        var info = new UserLoginInfo("GOOGLE", googleResponse.Subject, "GOOGLE");
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(googleResponse.Email);
            if (user is null)
            {
                user = User.Create(
                    new FirstName(googleResponse.FirstName),
                    new LastName(googleResponse.LastName),
                    googleResponse.Email,
                    googleResponse.Email,
                    _dateTimeProvider.Now,
                    false,
                    true);

                var createUserResult = await _userManager.CreateAsync(user);

                if (!createUserResult.Succeeded)
                {
                    var errors = createUserResult.Errors
                    .Select(authenticationFailure => new AuthenticationError(
                        authenticationFailure.Code,
                        authenticationFailure.Description))
                    .ToList();

                    throw new AuthenticationException(errors);
                }


                var addRoleRsult = await _userManager.AddToRoleAsync(user, "User");

                if (!addRoleRsult.Succeeded)
                {
                    var errors = addRoleRsult.Errors
                    .Select(authenticationFailure => new AuthenticationError(
                        authenticationFailure.Code,
                        authenticationFailure.Description))
                    .ToList();

                    throw new AuthenticationException(errors);
                }

                await _userManager.AddLoginAsync(user, info);
            }
            else
            {
                await _userManager.AddLoginAsync(user, info);
            }
        }

        if (user is null)
        {
            return Result.Failure<TokenResponse>(new Error("Token.CantVarifyToken", "Cant varify token."));
        }

        if (user.IsBanned)
        {
            if (user.BanEnd > _dateTimeProvider.Now)
            {
                return Result.Failure<TokenResponse>(UserErrors.Banned(user.BanEnd));
            }

            user.UnBan();

            await _userManager.UpdateAsync(user);
        }

        var accessToken = await _jwtService.CreateAccessToken(user);
        var rereshToken = await _jwtService.CreateRefreshToken(user);

        var tokenResponse = new TokenResponse { AccessToken = accessToken, RefreshToken = rereshToken };

        return tokenResponse;
    }
}