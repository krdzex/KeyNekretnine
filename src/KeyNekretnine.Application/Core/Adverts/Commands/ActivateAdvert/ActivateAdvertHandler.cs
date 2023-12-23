using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agents;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ActivateAdvert;
internal sealed class ActivateAdvertHandler : ICommandHandler<ActivateAdvertCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAgentRepository _agentRepository;

    public ActivateAdvertHandler(
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

    public async Task<Result> Handle(ActivateAdvertCommand request, CancellationToken cancellationToken)
    {
        var advert = await _advertRepository.GetByReferenceIdAsync(request.ReferenceId, cancellationToken);

        if (advert is null)
        {
            return Result.Failure(AdvertErrors.NotFound);
        }

        var canUserEditResult = await advert.CanCurrentUserUpdate(
            request.IsAgency,
            request.UserId,
            _agentRepository);

        if (canUserEditResult.IsFailure)
        {
            return canUserEditResult;
        }


        var activateResult = advert.Activate(_dateTimeProvider.Now);

        if (activateResult.IsFailure)
        {
            return activateResult;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}