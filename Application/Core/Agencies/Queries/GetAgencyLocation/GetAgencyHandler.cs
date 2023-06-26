using Application.Abstraction.Messaging;
using Contracts;
using Shared.DataTransferObjects.Agency;
using Shared.Error;

namespace Application.Core.Agencies.Queries.GetAgencyLocation;
internal sealed class GetAgencyHandler : IQueryHandler<GetAgencyLocationQuery, AgencyLocationDto>
{
    private readonly IRepositoryManager _repository;

    public GetAgencyHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<AgencyLocationDto>> Handle(GetAgencyLocationQuery request, CancellationToken cancellationToken)
    {
        var agencyLocation = await _repository.Agency.GetAgencyLocation(request.AgencyId, cancellationToken);

        return agencyLocation;
    }
}