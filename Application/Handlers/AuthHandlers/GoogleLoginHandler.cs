using Application.Commands.AuthCommands;
using Contracts;
using MediatR;
using Service.Contracts;
using Shared.RequestFeatures;

namespace Application.Handlers.AuthHandlers;
internal sealed class GoogleLoginHandler : IRequestHandler<GoogleLoginCommand, TokenRequest>
{
    private readonly IServiceManager _service;
    private readonly IRepositoryManager _repository;

    public GoogleLoginHandler(IServiceManager service, IRepositoryManager repository)
    {
        _service = service;
        _repository = repository;
    }


    public async Task<TokenRequest> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var payload = await _service.TokenService.VerifyGoogleToken(request.GoogleLoginDto);

        var user = await _service.AuthenticationService.GoogleLogin(request.GoogleLoginDto, payload);

        await _repository.User.BanCheck(user);

        var accessToken = await _service.TokenService.CreateToken(user);
        var rereshToken = await _service.TokenService.CreateRefreshToken(user);

        var tokenResponse = new TokenRequest { AccessToken = accessToken, RefreshToken = rereshToken };

        return tokenResponse;
    }

}
