using Application.Commands.AuthCommands;
using Contracts;
using MediatR;
using Service.Contracts;
using Shared.RequestFeatures;

namespace Application.Handlers.AuthHandlers;
internal sealed class LoginUserHandler : IRequestHandler<LoginUserCommand, TokenRequest>
{
    private readonly IServiceManager _service;
    private readonly IRepositoryManager _repository;

    public LoginUserHandler(IServiceManager service, IRepositoryManager repository)
    {
        _service = service;
        _repository = repository;
    }

    public async Task<TokenRequest> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var loggedUser = await _service.AuthenticationService.ValidateUser(request.LoginUser);

        await _repository.User.BanCheck(loggedUser);

        var accessToken = await _service.TokenService.CreateToken(loggedUser);
        var rereshToken = await _service.TokenService.CreateRefreshToken(loggedUser);

        var tokenResponse = new TokenRequest { AccessToken = accessToken, RefreshToken = rereshToken };

        return tokenResponse;
    }
}

