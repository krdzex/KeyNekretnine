using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Shared;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace KeyNekretnine.Application.Core.Auth.Commands.UserLogin;
internal sealed class LoginUserHandler : ICommandHandler<LoginUserCommand, TokenResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LoginUserHandler(IJwtService jwtService, UserManager<User> userManager, IDateTimeProvider dateTimeProvider)
    {
        _jwtService = jwtService;
        _userManager = userManager;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<TokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure<TokenResponse>(UserErrors.InvalidCredentials);
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result.Failure<TokenResponse>(UserErrors.InvalidCredentials);
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