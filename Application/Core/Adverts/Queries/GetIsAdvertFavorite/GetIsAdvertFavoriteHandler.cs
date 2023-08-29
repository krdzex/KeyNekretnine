using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetIsAdvertFavorite;
internal sealed class GetIsAdvertFavoriteHandler : IQueryHandler<GetIsAdvertFavoriteQuery, bool>
{
    private readonly IRepositoryManager _repository;

    public GetIsAdvertFavoriteHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(GetIsAdvertFavoriteQuery request, CancellationToken cancellationToken)
    {
        var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

        if (userId is null)
        {
            return Result.Failure<bool>(DomainErrors.User.UserNotFound);
        }

        var isFavorite = await _repository.Advert.ChackIfAdvertIsFavorite(userId, request.AdvertId, cancellationToken);

        return isFavorite;
    }
}