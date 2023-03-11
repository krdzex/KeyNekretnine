using Application.Queries.AdvertQueries;
using Contracts;
using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetFavoriteAdvertsHandler : IRequestHandler<GetFavoriteAdvertsQuery, Pagination<MinimalInformationsAboutAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetFavoriteAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Pagination<MinimalInformationsAboutAdvertDto>> Handle(GetFavoriteAdvertsQuery request, CancellationToken cancellationToken)
    {
        var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

        var adverts = await _repository.Advert.GetFavoriteAdverts(request.RequestParameters, userId, cancellationToken);

        return adverts;
    }
}
