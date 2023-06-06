using Application.Commands.AuthCommands;
using Contracts;
using MediatR;
using Service.Contracts;
using Shared.RequestFeatures;

namespace Application.Handlers.AuthHandlers;
internal sealed class FacebookLoginHandler : IRequestHandler<FacebookLoginCommand, TokenRequest>
{
    private readonly IServiceManager _service;
    private readonly IRepositoryManager _repository;

    public FacebookLoginHandler(IServiceManager service, IRepositoryManager repository)
    {
        _service = service;
        _repository = repository;
    }


    public async Task<TokenRequest> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
    {


        var fbUser = await _service.TokenService.VerifyFacebookTokenAndGetUserInfo(request.FbAccessToken);

        var dbUser = await _service.AuthenticationService.FacebookLogin(fbUser);

        await _repository.User.BanCheck(dbUser);

        var accessToken = await _service.TokenService.CreateToken(dbUser);
        var rereshToken = await _service.TokenService.CreateRefreshToken(dbUser);

        var tokenResponse = new TokenRequest { AccessToken = accessToken, RefreshToken = rereshToken };

        return tokenResponse;
    }

}
