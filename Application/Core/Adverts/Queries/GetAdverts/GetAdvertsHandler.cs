using Application.Abstraction.Messaging;
using Contracts;
using Entities.Exceptions;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace Application.Core.Adverts.Queries.GetAdverts;
internal sealed class GetAdvertsHandler : IQueryHandler<GetAdvertsQuery, Pagination<MinimalInformationsAboutAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Pagination<MinimalInformationsAboutAdvertDto>>> Handle(GetAdvertsQuery request, CancellationToken cancellationToken)
    {
        if (request.AdvertParameters.MaxPrice < request.AdvertParameters.MinPrice)
        {
            throw new BadPriceException();
        }

        if (request.AdvertParameters.MaxFloorSpace < request.AdvertParameters.MinFloorSpace)
        {
            throw new BadFloorSpaceException();
        }

        var adverts = await _repository.Advert.GetAdverts(request.AdvertParameters, cancellationToken);

        return adverts;
    }
}