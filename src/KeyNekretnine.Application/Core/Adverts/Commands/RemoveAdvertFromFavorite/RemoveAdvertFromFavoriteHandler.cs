using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Users;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RemoveAdvertFromFavorite;
internal sealed class RemoveAdvertFromFavoriteHandler : ICommandHandler<RemoveAdvertFromFavoriteCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserContext _userContext;

    public RemoveAdvertFromFavoriteHandler(
        IAdvertRepository advertRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider timeProvider,
        IUserContext userContext)
    {
        _advertRepository = advertRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _timeProvider = timeProvider;
        _userContext = userContext;
    }

    public async Task<Result> Handle(RemoveAdvertFromFavoriteCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetAcceptedAdvertByReferenceIdAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFound);
        }

        var user = await _userRepository.GetByIdWithFavoriteAdvertsAsync(_userContext.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        var removeAdvertFromAdvertResult = user.RemoveAdvertFromFavorites(advert.Id);

        if (removeAdvertFromAdvertResult.IsFailure)
        {
            return removeAdvertFromAdvertResult;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}