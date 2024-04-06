using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Adverts.Queries.GetLocationUpdate;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using KeyNekretnine.Domain.ValueObjects;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveLocationUpdate;
internal sealed class ApproveLocationUpdateHandler : ICommandHandler<ApproveLocationUpdateCommand>
{
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApproveLocationUpdateHandler(
        IAdvertUpdateRepository advertUpdateRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _advertUpdateRepository = advertUpdateRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(ApproveLocationUpdateCommand request, CancellationToken cancellationToken)
    {
        var update = await _advertUpdateRepository.GetByIdWithAdvert(request.UpdateId, UpdateTypes.Location, cancellationToken);

        if (update is null)
        {
            return Result.Failure(AdvertErrors.LocationUpdateNotFound);
        }

        var newValues = JsonConvert.DeserializeObject<LocationAdvertInformations>(update.Content)!;

        var approveResult = update.ApproveLocationUpdate(
            _dateTimeProvider.Now,
            Location.Create(
                newValues.Address,
                newValues.Latitude,
                newValues.Longitude),
            newValues.NeighborhoodId);

        if (approveResult.IsFailure)
        {
            return approveResult;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}