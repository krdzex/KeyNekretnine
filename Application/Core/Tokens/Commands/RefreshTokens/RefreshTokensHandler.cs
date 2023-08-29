using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Service.Contracts;
using Shared.Error;
using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Tokens.Commands.RefreshTokens;
internal sealed class RefreshTokensHandler : ICommandHandler<RefreshTokensCommand, TokenRequest>
{
    private readonly IServiceManager _service;

    public RefreshTokensHandler(IServiceManager service)
    {
        _service = service;
    }

    public async Task<Result<TokenRequest>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var newTokens = await _service.TokenService.VerifyRefreshToken(request.OldTokens);

        if (newTokens is null)
        {
            return Result.Failure<TokenRequest>(DomainErrors.Token.BadToken);
        }

        return newTokens;
    }
}