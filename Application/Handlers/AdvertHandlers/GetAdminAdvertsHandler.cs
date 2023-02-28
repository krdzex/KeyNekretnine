using Application.Queries.AdvertQueries;
using Contracts;
using MediatR;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetAdminAdvertsHandler : IRequestHandler<GetAdminAdvertsQuery, Pagination<AdminTableAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdminAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Pagination<AdminTableAdvertDto>> Handle(GetAdminAdvertsQuery request, CancellationToken cancellationToken)
    {

        var adverts = await _repository.Advert.GetAdminAdverts(request.AdminAdvertParameters, cancellationToken);

        return adverts;
    }
}
