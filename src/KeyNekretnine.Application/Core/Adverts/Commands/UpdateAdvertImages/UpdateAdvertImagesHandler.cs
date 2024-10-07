using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.TemporeryImageDatas;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertImages;
internal sealed class UpdateAdvertImagesHandler : ICommandHandler<UpdateAdvertImagesCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAgentRepository _agentRepository;
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUserContext _userContext;
    private readonly ITemporeryImageDataRepository _temporeryImageDataRepository;
    public UpdateAdvertImagesHandler(
        IAdvertRepository advertRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork,
        IAgentRepository agentRepository,
        IAdvertUpdateRepository advertUpdateRepository,
        IUserContext userContext,
        ITemporeryImageDataRepository temporeryImageDataRepository)
    {
        _advertRepository = advertRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _agentRepository = agentRepository;
        _advertUpdateRepository = advertUpdateRepository;
        _userContext = userContext;
        _temporeryImageDataRepository = temporeryImageDataRepository;
    }

    public async Task<Result> Handle(UpdateAdvertImagesCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdAsync(request.ReferenceId, cancellationToken);

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

        var canUserAddUpdate = await _advertUpdateRepository.CanAddUpdate(advert.Id, UpdateTypes.Image);

        if (!canUserAddUpdate)
        {
            return Result.Failure(AdvertErrors.ImageUpdateAlredyExist);
        }

        var updateAdvert = AdvertUpdate.Create(
            advert.Id,
            UpdateTypes.Image,
            _dateTimeProvider.Now,
            null,
            null);

        _advertUpdateRepository.Add(updateAdvert);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _temporeryImageDataRepository.BulkUpdateForUpdatingAdvert(request.ImageUpdateData.ImageIds, updateAdvert.Id, cancellationToken);

        return Result.Success();
    }
}