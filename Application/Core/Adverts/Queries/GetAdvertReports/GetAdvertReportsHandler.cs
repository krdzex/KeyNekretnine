using Application.Abstraction.Messaging;
using Contracts;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace Application.Core.Adverts.Queries.GetAdvertReports;
internal sealed class GetAdvertReportsHandler : IQueryHandler<GetAdvertReportsQuery, Pagination<AdvertReportsDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertReportsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Pagination<AdvertReportsDto>>> Handle(GetAdvertReportsQuery request, CancellationToken cancellationToken)
    {
        var advertReports = await _repository.Advert.GetAdvertReports(request.ReportParameters, cancellationToken);

        return advertReports;
    }
}