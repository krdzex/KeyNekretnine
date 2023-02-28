using Application.Commands.AdvertCommands;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class ApproveAdvertHandler : IRequestHandler<ApproveAdvertCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public ApproveAdvertHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(ApproveAdvertCommand request, CancellationToken cancellationToken)
    {

        var advertExist = await _repository.Advert.ChackIfAdvertExist(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            throw new AdvertNotFoundException(request.AdvertId);
        }

        await _repository.Advert.ApproveAdvert(request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}

