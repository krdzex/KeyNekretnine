using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using Service.Contracts;
using Shared.Error;
using Shared.RequestFeatures;
namespace Application.Core.Auth.Commands.FacebookLogin;
internal sealed class FacebookLoginHandler : ICommandHandler<FacebookLoginCommand, TokenRequest>
{
    private readonly IServiceManager _service;
    private readonly IRepositoryManager _repository;

    public FacebookLoginHandler(IServiceManager service, IRepositoryManager repository)
    {
        _service = service;
        _repository = repository;
    }


    public async Task<Result<TokenRequest>> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
    {
        var fbUser = await _service.TokenService.VerifyFacebookTokenAndGetUserInfo(request.FbAccessToken);

        var dbUser = await _service.AuthenticationService.FacebookLogin(fbUser);

        if (dbUser is null)
        {
            return Result.Failure<TokenRequest>(DomainErrors.Token.BadToken);
        }

        await _repository.User.BanCheck(dbUser);

        var accessToken = await _service.TokenService.CreateAccessToken(dbUser);
        var rereshToken = await _service.TokenService.CreateRefreshToken(dbUser);

        var tokenResponse = new TokenRequest { AccessToken = accessToken, RefreshToken = rereshToken };

        return tokenResponse;
    }

}
