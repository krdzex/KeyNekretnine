using Application.Queries.AdvertQueries;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetMyAdvertsHandler : IRequestHandler<GetMyAdvertsQuery, Pagination<MinimalInformationsAboutAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetMyAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Pagination<MinimalInformationsAboutAdvertDto>> Handle(GetMyAdvertsQuery request, CancellationToken cancellationToken)
    {
        var userId = await _repository.User.GetUserIdFromEmail(request.Email);

        if (userId == null)
        {
            throw new UserNotFoundException();
        }

        var adverts = await _repository.Advert.GetMyAdverts(request.AdvertParameters, userId, cancellationToken);

        return adverts;
    }
}
