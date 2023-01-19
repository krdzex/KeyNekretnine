using Application.Commands.UserCommands;
using Contracts;
using MediatR;
using Service.Contracts;
using Shared.RequestFeatures;

namespace Application.Handlers.UserHandlers;
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

        if (loggedUser.IsBanned)
        {
            if (loggedUser.BanEnd > DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException($"Banned until {loggedUser.BanEnd}");
            }
            await _repository.User.UserBanExpired(loggedUser, cancellationToken);
        }

        var accessToken = await _service.TokenService.CreateToken(loggedUser);
        var rereshToken = await _service.TokenService.CreateRefreshToken(loggedUser);

        var tokenResponse = new TokenRequest { Token = accessToken, RefreshToken = rereshToken };

        return tokenResponse;
    }
}

