using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agents;
using KeyNekretnine.Domain.Shared;

namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertLocation;
internal sealed class UpdateAdvertLocationHandler : ICommandHandler<UpdateAdvertLocationCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAgentRepository _agentRepository;

    public UpdateAdvertLocationHandler(
        IAdvertRepository advertRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IAgentRepository agentRepository)
    {
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _agentRepository = agentRepository;
    }

    public async Task<Result> Handle(UpdateAdvertLocationCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFoundForAdmin);
        }

        var canUserEditResult = await advert.CanCurrentUserUpdateAdvert(
            request.isAgency,
            request.UserId,
            _agentRepository);

        if (!canUserEditResult.IsSuccess)
        {
            return canUserEditResult;
        }

        advert.UpdateLocation(
            _dateTimeProvider.Now,
            new Location(
                request.Address,
                request.Latitude,
                request.Longitude),
            request.NeighborhoodId);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}