using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace Application.Adverts.Queries.GetAdvertById;
internal sealed class GetAdvertHandler : IQueryHandler<GetAdvertByIdQuery, AllInfomrationsAboutAdvertDto>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Result<AllInfomrationsAboutAdvertDto>> Handle(GetAdvertByIdQuery request, CancellationToken cancellationToken)
    {
        var advert = await _repository.Advert.GetAdvert(request.Id, cancellationToken);

        if (advert is null)
        {
            return Result.Failure<AllInfomrationsAboutAdvertDto>(DomainErrors.Advert.AdvertNotFound(request.Id));
        }

        return advert;
    }
}
