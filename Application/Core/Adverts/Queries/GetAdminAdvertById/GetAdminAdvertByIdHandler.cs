using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetAdminAdvertById;
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