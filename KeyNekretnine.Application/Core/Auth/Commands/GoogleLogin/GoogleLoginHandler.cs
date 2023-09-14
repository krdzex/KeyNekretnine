using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Service.Contracts;
using Shared.Error;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Auth.Commands.GoogleLogin;
internal sealed class GoogleLoginHandler : ICommandHandler<GoogleLoginCommand, TokenRequest>
{
    private readonly IServiceManager _service;
    private readonly IRepositoryManager _repository;

    public GoogleLoginHandler(IServiceManager service, IRepositoryManager repository)
    {
        _service = service;
        _repository = repository;
    }

    public async Task<Result<TokenRequest>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var payload = await _service.TokenService.VerifyGoogleToken(request.GoogleLoginDto);

        var user = await _service.AuthenticationService.GoogleLogin(request.GoogleLoginDto, payload);

        if (user is null)
        {
            return Result.Failure<TokenRequest>(DomainErrors.Token.BadToken);
        }

        await _repository.User.BanCheck(user);

        var accessToken = await _service.TokenService.CreateAccessToken(user);
        var rereshToken = await _service.TokenService.CreateRefreshToken(user);

        var tokenResponse = new TokenRequest { AccessToken = accessToken, RefreshToken = rereshToken };

        return tokenResponse;
    }
}