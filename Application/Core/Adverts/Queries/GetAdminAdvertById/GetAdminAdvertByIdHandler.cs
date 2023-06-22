using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace Application.Core.Adverts.Queries.GetAdminAdvert;
internal sealed class GetAdminAdvertByIdHandler : IQueryHandler<GetAdminAdvertByIdQuery, AdminAllInformationsAboutAdvertDto>
{
    private readonly IRepositoryManager _repository;

    public GetAdminAdvertByIdHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<AdminAllInformationsAboutAdvertDto>> Handle(GetAdminAdvertByIdQuery request, CancellationToken cancellationToken)
    {
        var advert = await _repository.Advert.GetAdminAdvertById(request.AdvertId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure<AdminAllInformationsAboutAdvertDto>(DomainErrors.Advert.AdminAdvertNotFound(request.AdvertId));
        }

        return advert;
    }
}