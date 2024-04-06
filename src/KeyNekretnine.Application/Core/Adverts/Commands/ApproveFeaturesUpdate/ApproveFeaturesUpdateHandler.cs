using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Adverts.Queries.GetFeaturesUpdate;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveFeaturesUpdate;
internal sealed class ApproveFeaturesUpdateHandler : ICommandHandler<ApproveFeaturesUpdateCommand>
{
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApproveFeaturesUpdateHandler(
        IAdvertUpdateRepository advertUpdateRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _advertUpdateRepository = advertUpdateRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(ApproveFeaturesUpdateCommand request, CancellationToken cancellationToken)
    {
        var update = await _advertUpdateRepository.GetByIdWithAdvertAndFeatures(request.UpdateId, UpdateTypes.Features, cancellationToken);

        if (update is null)
        {
            return Result.Failure(AdvertErrors.FeaturesUpdateNotFound);
        }

        var newValues = JsonConvert.DeserializeObject<FeaturesInformations>(update.Content)!;

        var approveResult = update.ApproveFeaturesUpdate(_dateTimeProvider.Now, newValues.Features);

        if (approveResult.IsFailure)
        {
            return approveResult;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}