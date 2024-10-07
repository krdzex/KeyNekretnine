using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectBasicUpdate;
internal sealed class RejectBasicUpdateHandler : ICommandHandler<RejectBasicUpdateCommand>
{
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RejectBasicUpdateHandler(
        IAdvertUpdateRepository advertUpdateRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _advertUpdateRepository = advertUpdateRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(RejectBasicUpdateCommand request, CancellationToken cancellationToken)
    {
        var update = await _advertUpdateRepository.GetByIdWithAdvert(request.UpdateId, UpdateTypes.BasicInformation, cancellationToken);

        if (update is null)
        {
            return Result.Failure(AdvertErrors.BasicUpdateNotFound);
        }

        var rejectResult = update.RejectBasicUpdate(_dateTimeProvider.Now);

        if (rejectResult.IsFailure)
        {
            return rejectResult;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}