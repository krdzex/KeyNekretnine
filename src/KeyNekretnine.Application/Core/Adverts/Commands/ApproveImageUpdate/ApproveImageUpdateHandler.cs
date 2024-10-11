using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Adverts.Queries.GetImageUpdate;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using KeyNekretnine.Domain.Images;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveImageUpdate;
internal sealed class ApproveImageUpdateHandler : ICommandHandler<ApproveImageUpdateCommand>
{
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApproveImageUpdateHandler(
        IAdvertUpdateRepository advertUpdateRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _advertUpdateRepository = advertUpdateRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(ApproveImageUpdateCommand request, CancellationToken cancellationToken)
    {
        var update = await _advertUpdateRepository.GetByIdWithAdvert(request.UpdateId, UpdateTypes.Image, cancellationToken);

        if (update is null)
        {
            return Result.Failure(AdvertErrors.BasicUpdateNotFound);
        }

        var imageUrls = JsonConvert.DeserializeObject<ImagesInformations>(update.NewContent)!.Images;

        var newImages = imageUrls
            .Select(url => new Image(url))
            .ToList();

        var result = update.ApproveImageUpdate(_dateTimeProvider.Now, newImages);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}