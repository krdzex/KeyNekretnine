using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdvertById;
internal sealed class GetMyAdvertByIdHandler : IQueryHandler<GetMyAdvertByIdQuery, MyAdvertDto>
{
    private readonly IRepositoryManager _repository;

    public GetMyAdvertByIdHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<MyAdvertDto>> Handle(GetMyAdvertByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

        if (userId is null)
        {
            return Result.Failure<MyAdvertDto>(DomainErrors.User.UserNotFound);
        }

        var advert = await _repository.Advert.GetMyAdvertById(request.AdvertId, userId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure<MyAdvertDto>(DomainErrors.Advert.AdvertNotFound(request.AdvertId));
        }

        return advert;
    }
}