using Application.Queries.AdvertQueries;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetMyAdvertHandler : IRequestHandler<GetMyAdvertQuery, MyAdvertDto>
{
    private readonly IRepositoryManager _repository;

    public GetMyAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<MyAdvertDto> Handle(GetMyAdvertQuery request, CancellationToken cancellationToken)
    {
        var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

        var advert = await _repository.Advert.GetMyAdvert(request.AdvertId, userId, cancellationToken);

        if (advert is null)
        {
            throw new AdvertNotFoundException(request.AdvertId);
        }

        return advert;
    }
}
