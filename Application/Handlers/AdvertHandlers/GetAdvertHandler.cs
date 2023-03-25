using Application.Queries.AdvertQuery;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetAdvertHandler : IRequestHandler<GetAdvertQuery, AllInfomrationsAboutAdvertDto>
{
    private readonly IRepositoryManager _repository;

    public GetAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<AllInfomrationsAboutAdvertDto> Handle(GetAdvertQuery request, CancellationToken cancellationToken)
    {
        var advert = await _repository.Advert.GetAdvert(request.Id, cancellationToken);

        if (advert is null)
        {
            throw new AdvertNotFoundException(request.Id);
        }

        return advert;
    }
}
