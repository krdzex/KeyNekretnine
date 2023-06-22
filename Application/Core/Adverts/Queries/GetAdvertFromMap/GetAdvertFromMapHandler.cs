using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace Application.Core.Adverts.Queries.GetAdvertFromMap;
internal sealed class GetAdvertFromMapHandler : IQueryHandler<GetAdvertFromMapQuery, MinimalInformationsAboutAdvertDto>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertFromMapHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<MinimalInformationsAboutAdvertDto>> Handle(GetAdvertFromMapQuery request, CancellationToken cancellationToken)
    {
        var advert = await _repository.Advert.GetAdvertFromMapPoint(request.Id, cancellationToken);

        if (advert is null)
        {
            return Result.Failure<MinimalInformationsAboutAdvertDto>(DomainErrors.Advert.AdvertNotFound(request.Id));

        }

        return advert;
    }
}