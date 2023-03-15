using Application.Queries.AdvertQueries;
using Contracts;
using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetAdvertReportsHandler : IRequestHandler<GetAdvertReportsQuery, Pagination<AdvertReportsDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertReportsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Pagination<AdvertReportsDto>> Handle(GetAdvertReportsQuery request, CancellationToken cancellationToken)
    {
        var advertReports = await _repository.Advert.GetAdvertReports(request.ReportParameters, cancellationToken);

        return advertReports;
    }
}
