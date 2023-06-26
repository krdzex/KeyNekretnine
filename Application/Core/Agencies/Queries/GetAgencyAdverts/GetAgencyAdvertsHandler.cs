using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace Application.Core.Agencies.Queries.GetAgencyAdverts;
internal sealed class GetAgencyAdvertsHandler : IQueryHandler<GetAgencyAdvertsQuery, List<MinimalInformationsAboutAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAgencyAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<MinimalInformationsAboutAdvertDto>>> Handle(GetAgencyAdvertsQuery request, CancellationToken cancellationToken)
    {
        var agencyExist = await _repository.Agency.DoesAgencyExist(request.AgencyId, cancellationToken);

        if (!agencyExist)
        {
            return Result.Failure<List<MinimalInformationsAboutAdvertDto>>(DomainErrors.Agency.AgencyNotFound);
        }

        var adverts = await _repository.Agency.GetAdvertsForAgency(request.AgencyId, cancellationToken);

        return adverts.ToList();
    }
}