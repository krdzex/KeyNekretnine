using Application.Commands.AdvertCommands;
using Contracts;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class ReportAdvertHandler : IRequestHandler<ReportAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public ReportAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(ReportAdvertCommand request, CancellationToken cancellationToken)
    {
        //if (request.AdvertParameters.MaxPrice < request.AdvertParameters.MinPrice)
        //{
        //    throw new BadPriceException();
        //}

        //if (request.AdvertParameters.MaxFloorSpace < request.AdvertParameters.MinFloorSpace)
        //{
        //    throw new BadFloorSpaceException();
        //}

        //var adverts = await _repository.Advert.GetAdverts(request.AdvertParameters, cancellationToken);

        return Unit.Value;
    }
}

