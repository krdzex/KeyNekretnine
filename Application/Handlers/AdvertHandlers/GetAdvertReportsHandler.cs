using Application.Queries.AdvertQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetAdvertReportsHandler : IRequestHandler<GetAdvertReportsQuery, IEnumerable<AdvertReportsDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertReportsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AdvertReportsDto>> Handle(GetAdvertReportsQuery request, CancellationToken cancellationToken)
    {
        var advertReports = await _repository.Advert.GetAdvertReports(cancellationToken);

        return advertReports;
    }
}
