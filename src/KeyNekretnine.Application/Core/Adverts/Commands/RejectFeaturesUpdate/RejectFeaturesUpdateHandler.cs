using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectFeaturesUpdate;
internal sealed class RejectFeaturesUpdateHandler : ICommandHandler<RejectFeaturesUpdateCommand>
{
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RejectFeaturesUpdateHandler(
        IAdvertUpdateRepository advertUpdateRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _advertUpdateRepository = advertUpdateRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(RejectFeaturesUpdateCommand request, CancellationToken cancellationToken)
    {
        var update = await _advertUpdateRepository.GetByIdWithAdvert(request.UpdateId, UpdateTypes.Feature, cancellationToken);

        if (update is null)
        {
            return Result.Failure(AdvertErrors.FeaturesUpdateNotFound);
        }

        var rejectResult = update.RejectFeaturesUpdate(_dateTimeProvider.Now);

        if (rejectResult.IsFailure)
        {
            return rejectResult;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}