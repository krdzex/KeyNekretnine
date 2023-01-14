using Application.Queries;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.CustomResponses;

namespace Application.Handlers;
internal sealed class GetAdvertsInPaginationHandler : IRequestHandler<GetAdvertsInPaginationQuery, Pagination>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertsInPaginationHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Pagination> Handle(GetAdvertsInPaginationQuery request, CancellationToken cancellationToken)
    {
        if (!request.AdvertParameters.ValidPriceRange)
        {
            throw new BadPriceException();
        }
        var adverts = await _repository.Advert.GetAdverts(request.AdvertParameters);

        return adverts;
    }
}
