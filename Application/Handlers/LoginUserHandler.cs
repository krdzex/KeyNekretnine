using Application.Commands;
using MediatR;
using Service.Contracts;
using Shared.RequestFeatures;

namespace Application.Handlers;
internal sealed class LoginUserHandler : IRequestHandler<LoginUserCommand, TokenRequest>
{
    private readonly IServiceManager _service;

    public LoginUserHandler(IServiceManager service)
    {
        _service = service;
    }

    public async Task<TokenRequest> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var loggedUser = await _service.AuthenticationService.ValidateUser(request.LoginUser);

        if (loggedUser is null)
        {
            throw new UnauthorizedAccessException("Invalid Credentials");
        }

        var accessToken = await _service.TokenService.CreateToken(loggedUser);
        var rereshToken = await _service.TokenService.CreateRefreshToken(loggedUser);

        var tokenResponse = new TokenRequest { Token = accessToken, RefreshToken = rereshToken };

        return tokenResponse;
    }
}

