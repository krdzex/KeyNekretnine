using Application.Commands.AdvertCommands;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers.AdvertHandlers;
internal sealed class RemoveAdvertFromFavoriteHandler : IRequestHandler<RemoveAdvertFromFavoriteCommand, Unit>
{

    private readonly IRepositoryManager _repository;
    public RemoveAdvertFromFavoriteHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(RemoveAdvertFromFavoriteCommand request, CancellationToken cancellationToken)
    {

        var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail, cancellationToken);

        var isFavorite = await _repository.Advert.ChackIfAdvertIsFavorite(userId, request.AdvertId, cancellationToken);

        if (!isFavorite)
        {
            throw new AdvertNotFavoriteException();
        }

        await _repository.Advert.RemoveAdvertFromFavorite(userId, request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}

