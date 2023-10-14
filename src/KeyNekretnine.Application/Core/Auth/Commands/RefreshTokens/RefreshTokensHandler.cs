using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Application.Core.Auth.Commands.RefreshTokens;
internal sealed class RefreshTokensHandler : ICommandHandler<RefreshTokensCommand, RefreshTokenResponse>
{
    private readonly IJwtService _jwtService;
    public RefreshTokensHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var newTokens = await _jwtService.VerifyRefreshToken(request.AccessToken, request.RefreshToken);

        if (newTokens is null)
        {
            return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidToken);
        }

        return newTokens;
    }
}