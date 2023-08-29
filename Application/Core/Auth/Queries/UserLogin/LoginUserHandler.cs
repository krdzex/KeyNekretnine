using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Service.Contracts;
using Shared.Error;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Auth.Queries.UserLogin;
internal sealed class LoginUserHandler : IQueryHandler<LoginUserQuery, TokenRequest>
{
    private readonly IServiceManager _service;
    private readonly IRepositoryManager _repository;

    public LoginUserHandler(IServiceManager service, IRepositoryManager repository)
    {
        _service = service;
        _repository = repository;
    }

    public async Task<Result<TokenRequest>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var loggedUser = await _service.AuthenticationService.ValidateUser(request.LoginUser);

        if (loggedUser is null)
        {
            return Result.Failure<TokenRequest>(DomainErrors.Auth.InvalidCredentials);
        }

        await _repository.User.BanCheck(loggedUser);

        var accessToken = await _service.TokenService.CreateAccessToken(loggedUser);
        var rereshToken = await _service.TokenService.CreateRefreshToken(loggedUser);

        var tokenResponse = new TokenRequest { AccessToken = accessToken, RefreshToken = rereshToken };

        return tokenResponse;
    }
}