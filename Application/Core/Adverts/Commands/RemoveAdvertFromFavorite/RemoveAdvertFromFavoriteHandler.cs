using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Shared.Error;

namespace Application.Core.Adverts.Commands.RemoveAdvertFromFavorite;
internal sealed class RemoveAdvertFromFavoriteHandler : ICommandHandler<RemoveAdvertFromFavoriteCommand, Unit>
{

    private readonly IRepositoryManager _repository;
    public RemoveAdvertFromFavoriteHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<Result<Unit>> Handle(RemoveAdvertFromFavoriteCommand request, CancellationToken cancellationToken)
    {

        var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail, cancellationToken);

        if (userId is null)
        {
            return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
        }

        var isFavorite = await _repository.Advert.ChackIfAdvertIsFavorite(userId, request.AdvertId, cancellationToken);

        if (!isFavorite)
        {
            return Result.Failure<Unit>(DomainErrors.Advert.AdvertNotFavorite);
        }

        await _repository.Advert.RemoveAdvertFromFavorite(userId, request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}

