using Application.Commands.AdvertCommands;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class DeclineAdvertHandler : IRequestHandler<DeclineAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public DeclineAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeclineAdvertCommand request, CancellationToken cancellationToken)
    {
        var advertExist = await _repository.Advert.ChackIfAdvertExist(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            throw new AdvertNotFoundException(request.AdvertId);
        }

        await _repository.Advert.DeclineAdvert(request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}
