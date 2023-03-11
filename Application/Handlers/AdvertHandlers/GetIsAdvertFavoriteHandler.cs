using Application.Queries.AdvertQueries;
using Contracts;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetIsAdvertFavoriteHandler : IRequestHandler<GetIsFavoriteAdvertQuery, bool>
{
    private readonly IRepositoryManager _repository;

    public GetIsAdvertFavoriteHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(GetIsFavoriteAdvertQuery request, CancellationToken cancellationToken)
    {
        var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

        var isFavorite = await _repository.Advert.ChackIfAdvertIsFavorite(userId, request.AdvertId, cancellationToken);

        return isFavorite;
    }
}

