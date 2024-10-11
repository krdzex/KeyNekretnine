using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Application.Core.Adverts.Queries.GetImageUpdate;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Commands.RejectImageUpdate;
internal sealed class RejectImageUpdateHandler : ICommandHandler<RejectImageUpdateCommand>
{
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IImageToDeleteRepository _imageToDeleteRepository;
    public RejectImageUpdateHandler(
        IAdvertUpdateRepository advertUpdateRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IImageToDeleteRepository imageToDeleteRepository)
    {
        _advertUpdateRepository = advertUpdateRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _imageToDeleteRepository = imageToDeleteRepository;
    }

    public async Task<Result> Handle(RejectImageUpdateCommand request, CancellationToken cancellationToken)
    {
        var update = await _advertUpdateRepository.GetByIdWithAdvert(request.UpdateId, UpdateTypes.Image, cancellationToken);

        if (update is null)
        {
            return Result.Failure(AdvertErrors.BasicUpdateNotFound);
        }

        var imageUrls = JsonConvert.DeserializeObject<ImagesInformations>(update.NewContent)!.Images;

        await _imageToDeleteRepository.AddMultipleAsync(imageUrls, _dateTimeProvider.Now, cancellationToken);

        _advertUpdateRepository.Remove(update);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}