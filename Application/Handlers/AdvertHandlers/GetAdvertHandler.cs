using Application.Queries.AdvertQuery;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetAdvertHandler : IRequestHandler<GetAdvertQuery, AllInfomrationsAboutAdvertDto>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<AllInfomrationsAboutAdvertDto> Handle(GetAdvertQuery request, CancellationToken cancellationToken)
    {
        var advert = await _repository.Advert.GetAdvert(request.Id);

        if (!String.IsNullOrEmpty(request.Email))
        {
            var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

            advert.Is_Favorite = await _repository.Advert.ChackIfAdvertIsFavorite(userId, advert.Id, cancellationToken);
        }

        return advert;
    }
}
