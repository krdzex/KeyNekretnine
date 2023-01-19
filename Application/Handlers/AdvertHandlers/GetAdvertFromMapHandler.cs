using Application.Queries.AdvertQuery;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetAdvertFromMapHandler : IRequestHandler<GetAdvertFromMapQuery, MinimalInformationsAboutAdvertDto>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertFromMapHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<MinimalInformationsAboutAdvertDto> Handle(GetAdvertFromMapQuery request, CancellationToken cancellationToken)
    {
        var advert = await _repository.Advert.GetAdvertFromMapPoint(request.Id, cancellationToken);

        return advert;
    }
}

