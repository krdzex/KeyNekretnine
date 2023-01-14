using Application.Commands;
using MediatR;
using Service.Contracts;
using Shared.RequestFeatures;

namespace Application.Handlers;
internal sealed class CreateAccessAndRefreshTokenHandler : IRequestHandler<CreateAccessAndRefreshTokenCommand, TokenRequest>
{
    private readonly IServiceManager _service;

    public CreateAccessAndRefreshTokenHandler(IServiceManager service)
    {
        _service = service;
    }
    public async Task<TokenRequest> Handle(CreateAccessAndRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var newTokens = await _service.TokenService.VerifyRefreshToken(request.OldTokens);

        if (newTokens is null)
        {
            throw new UnauthorizedAccessException("Cant verify token");
        }

        return newTokens;
    }
}