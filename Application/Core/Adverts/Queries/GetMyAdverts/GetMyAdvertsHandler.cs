using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Adverts.Queries.GetMyAdverts;
internal sealed class GetMyAdvertsHandler : IQueryHandler<GetMyAdvertsQuery, Pagination<MyAdvertsDto>>
{
    private readonly IRepositoryManager _repository;

    public GetMyAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Pagination<MyAdvertsDto>>> Handle(GetMyAdvertsQuery request, CancellationToken cancellationToken)
    {
        var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

        if (userId == null)
        {
            return Result.Failure<Pagination<MyAdvertsDto>>(DomainErrors.User.UserNotFound);
        }

        var adverts = await _repository.Advert.GetMyAdverts(request.MyAdvertParameters, userId, cancellationToken);

        return adverts;
    }
}