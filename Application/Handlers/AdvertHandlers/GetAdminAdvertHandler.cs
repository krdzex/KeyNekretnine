using Application.Queries.AdvertQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.Advert;

namespace Application.Handlers.AdvertHandlers;
internal sealed class GetAdminAdvertHandler : IRequestHandler<GetAdminAdvertQuery, AdminAllInformationsAboutAdvertDto>
{
    private readonly IRepositoryManager _repository;

    public GetAdminAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<AdminAllInformationsAboutAdvertDto> Handle(GetAdminAdvertQuery request, CancellationToken cancellationToken)
    {
        var advert = await _repository.Advert.GetAdminAdvert(request.AdvertId);

        return advert;
    }
}
