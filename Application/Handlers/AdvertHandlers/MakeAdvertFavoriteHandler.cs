using Application.Commands.AdvertCommands;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class MakeAdvertFavoriteHandler : IRequestHandler<MakeAdvertFavoriteCommand, Unit>
{

    private readonly IRepositoryManager _repository;
    public MakeAdvertFavoriteHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(MakeAdvertFavoriteCommand request, CancellationToken cancellationToken)
    {
        var advertExist = await _repository.Advert.ChackIfAdvertExistAndItsApproved(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            throw new AdvertNotFoundException(request.AdvertId);
        }

        var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail);

        var isFavoriteAlready = await _repository.Advert.ChackIfAdvertIsFavorite(userId, request.AdvertId, cancellationToken);

        if (isFavoriteAlready)
        {
            throw new AdvertAlreadyFavoriteException();
        }

        await _repository.Advert.MakeAdvertFavorite(userId, request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}

