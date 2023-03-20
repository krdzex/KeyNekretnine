using Application.Queries.AdvertQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetCompareAdvertsHandler : IRequestHandler<GetAdvertsCompareQuery, IEnumerable<AllInfomrationsAboutAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetCompareAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AllInfomrationsAboutAdvertDto>> Handle(GetAdvertsCompareQuery request, CancellationToken cancellationToken)
    {
        var adverts = await _repository.Advert.GetAdminAdvert(request.AdvertId);

        return adverts;
    }
}
