using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetFavoriteAdverts;
internal sealed class GetFavoriteAdvertsHandler : IQueryHandler<GetFavoriteAdvertsQuery, Pagination<MinimalInformationsAboutAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetFavoriteAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Pagination<MinimalInformationsAboutAdvertDto>>> Handle(GetFavoriteAdvertsQuery request, CancellationToken cancellationToken)
    {
        var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

        if (userId is null)
        {
            return Result.Failure<Pagination<MinimalInformationsAboutAdvertDto>>(DomainErrors.User.UserNotFound);
        }

        var adverts = await _repository.Advert.GetFavoriteAdverts(request.RequestParameters, userId, cancellationToken);

        return adverts;
    }
}