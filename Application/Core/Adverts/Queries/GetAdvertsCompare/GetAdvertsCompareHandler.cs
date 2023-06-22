using Application.Abstraction.Messaging;
using Application.Core.Adverts.Queries.GetADvertsCompare;
using Contracts;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace Application.Core.Adverts.Queries.GetAdvertsCompare;
internal sealed class GetAdvertsCompareHandler : IQueryHandler<GetAdvertsCompareQuery, List<CompareAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertsCompareHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<CompareAdvertDto>>> Handle(GetAdvertsCompareQuery request, CancellationToken cancellationToken)
    {
        var adverts = await _repository.Advert.GetAdvertsCompare(request.FirstAdvert, request.SacondAdvert, cancellationToken);

        return adverts.ToList();
    }
}