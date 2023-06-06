using Application.Abstraction.Messaging;
using Contracts;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.Error;

namespace Application.Core.Adverts.Queries.GetAdminAdverts;
internal sealed class GetAdminAdvertsHandler : IQueryHandler<GetAdminAdvertsQuery, Pagination<AdminTableAdvertDto>>
{
    private readonly IRepositoryManager _repository;

    public GetAdminAdvertsHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Result<Pagination<AdminTableAdvertDto>>> Handle(GetAdminAdvertsQuery request, CancellationToken cancellationToken)
    {

        var adverts = await _repository.Advert.GetAdminAdverts(request.AdminAdvertParameters, cancellationToken);

        return adverts;
    }
}
