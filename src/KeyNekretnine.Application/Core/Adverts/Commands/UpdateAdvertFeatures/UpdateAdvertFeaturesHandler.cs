using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.AdvertUpdates;
using KeyNekretnine.Domain.Agents;
using Newtonsoft.Json;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertFeatures;
internal sealed class UpdateAdvertFeaturesHandler : ICommandHandler<UpdateAdvertFeaturesCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAgentRepository _agentRepository;
    private readonly IAdvertUpdateRepository _advertUpdateRepository;
    private readonly IUserContext _userContext;

    public UpdateAdvertFeaturesHandler(
        IAdvertRepository advertRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork,
        IAgentRepository agentRepository,
        IAdvertUpdateRepository advertUpdateRepository,
        IUserContext userContext)
    {
        _advertRepository = advertRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _agentRepository = agentRepository;
        _advertUpdateRepository = advertUpdateRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UpdateAdvertFeaturesCommand request, CancellationToken cancellationToken)
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

        var canUserAddUpdate = await _advertUpdateRepository.CanAddUpdate(advert.Id, UpdateTypes.Features);

        if (!canUserAddUpdate)
        {
            return Result.Failure(AdvertErrors.BasicUpdateAlredyExist);
        }

        var updateAdvert = AdvertUpdate.Create(
            advert.Id,
            UpdateTypes.Features,
            _dateTimeProvider.Now,
            JsonConvert.SerializeObject(request.FeaturesUpdateData));

        _advertUpdateRepository.Add(updateAdvert);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}