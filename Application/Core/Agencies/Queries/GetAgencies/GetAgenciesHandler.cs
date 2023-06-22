using Application.Abstraction.Messaging;
using Contracts;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Agency;
using Shared.Error;

namespace Application.Core.Agencies.Queries.GetAgencies;
internal sealed class GetAgenciesHandler : IQueryHandler<GetAgenciesQuery, Pagination<GetAgenciesDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAgenciesHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Pagination<GetAgenciesDto>>> Handle(GetAgenciesQuery request, CancellationToken cancellationToken)
    {
        var agencies = await _repository.Agency.GetAgencies(request.AgencyParameters, cancellationToken);

        return agencies;
    }
}
