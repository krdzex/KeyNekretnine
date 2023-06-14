using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using Shared.DataTransferObjects.Agency;
using Shared.Error;

namespace Application.Core.Agencies.Queries.GetAgencyById;

internal sealed class GetAgencyByIdHandler : IQueryHandler<GetAgencyByIdQuery, GetAgencyDto>
{
    private readonly IRepositoryManager _repository;

    public GetAgencyByIdHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetAgencyDto>> Handle(GetAgencyByIdQuery request, CancellationToken cancellationToken)
    {
        var agency = await _repository.Agency.GetAgencyById(request.AgencyId, cancellationToken);

        if (agency is null)
        {
            return Result.Failure<GetAgencyDto>(DomainErrors.Agency.AgencyNotFound);
        }

        return agency;
    }
}