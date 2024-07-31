using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Image;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agents;

namespace KeyNekretnine.Application.Core.Adverts.Commands.DeleteImagesCommand;
internal sealed class DeleteImagesHandler : ICommandHandler<DeleteImagesCommand>
{
    private readonly IImageToDeleteRepository _imageToDeleteRepository;
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly IAgentRepository _agentRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    public DeleteImagesHandler(
        IImageToDeleteRepository imageToDeleteRepository,
        IAdvertRepository advertRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        IAgentRepository agentRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _imageToDeleteRepository = imageToDeleteRepository;
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _agentRepository = agentRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(DeleteImagesCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdWithImagesAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFound);
        }

        var canUserEditResult = await advert.CanCurrentUserUpdate(
            _userContext.AgencyId is not null,
            _userContext.UserId,
            _agentRepository);

        if (canUserEditResult.IsFailure)
        {
            return canUserEditResult;
        }

        var realImagesToDelete = advert.AdvertImages.Where(x => request.ImageUrls.Contains(x.Url)).ToList();

        if (advert.AdvertImages.Count - realImagesToDelete.Count() < 2)
        {
            return Result.Failure(AdvertErrors.NoEnoughImages);
        }

        foreach (var imageToDelete in realImagesToDelete)
        {
            var removeImageFromAdvertResult = advert.RemoveImage(imageToDelete, _dateTimeProvider.Now);

            if (removeImageFromAdvertResult.IsFailure)
            {
                return removeImageFromAdvertResult;
            }

            await _imageToDeleteRepository.AddAsync(imageToDelete.Url, _dateTimeProvider.Now, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}