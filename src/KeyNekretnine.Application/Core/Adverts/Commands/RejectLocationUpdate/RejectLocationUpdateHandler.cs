using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectLocationUpdate;
internal sealed class RejectLocationUpdateHandler : ICommandHandler<RejectLocationUpdateCommand>
{
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RejectLocationUpdateHandler(
        IAdvertUpdateRepository advertUpdateRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _advertUpdateRepository = advertUpdateRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(RejectLocationUpdateCommand request, CancellationToken cancellationToken)
    {
        var update = await _advertUpdateRepository.GetByIdWithAdvert(request.UpdateId, UpdateTypes.Location, cancellationToken);

        if (update is null)
        {
            return Result.Failure(AdvertErrors.BasicUpdateNotFound);
        }

        var rejectResult = update.RejectLocationUpdate(_dateTimeProvider.Now);

        if (rejectResult.IsFailure)
        {
            return rejectResult;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}