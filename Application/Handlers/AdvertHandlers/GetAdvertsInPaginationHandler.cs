using Application.Queries.AdvertQuery;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetAdvertsInPaginationHandler : IRequestHandler<GetAdvertsInPaginationQuery, Pagination<MinimalInformationsAboutAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertsInPaginationHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Pagination<MinimalInformationsAboutAdvertDto>> Handle(GetAdvertsInPaginationQuery request, CancellationToken cancellationToken)
    {
        if (request.AdvertParameters.MaxPrice < request.AdvertParameters.MinPrice)
        {
            throw new BadPriceException();
        }
        if (request.AdvertParameters.MaxFloorSpace < request.AdvertParameters.MinFloorSpace)
        {
            throw new BadFloorSpaceException();
        }
        var adverts = await _repository.Advert.GetAdverts(request.AdvertParameters);

        return adverts;
    }
}
