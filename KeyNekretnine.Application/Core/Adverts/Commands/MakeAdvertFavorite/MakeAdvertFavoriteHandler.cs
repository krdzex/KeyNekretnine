using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.Error;

namespace KeyNekretnine.Application.Core.Adverts.Commands.MakeAdvertFavorite;
internal sealed class MakeAdvertFavoriteHandler : ICommandHandler<MakeAdvertFavoriteCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public MakeAdvertFavoriteHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(MakeAdvertFavoriteCommand request, CancellationToken cancellationToken)
    {
        var advertExist = await _repository.Advert.ChackIfAdvertExistAndItsApproved(request.AdvertId, cancellationToken);

        if (!advertExist)
        {
            return Result.Failure<Unit>(DomainErrors.Advert.AdvertNotFound(request.AdvertId));
        }

        var userId = await _repository.User.GetUserIdFromEmail(request.UserEmail, cancellationToken);

        if (userId is null)
        {
            return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
        }

        var isFavorite = await _repository.Advert.ChackIfAdvertIsFavorite(userId, request.AdvertId, cancellationToken);

        if (isFavorite)
        {
            return Result.Failure<Unit>(DomainErrors.Advert.AdvertAlreadyFavorite);
        }

        await _repository.Advert.MakeAdvertFavorite(userId, request.AdvertId, cancellationToken);

        return Unit.Value;
    }
}