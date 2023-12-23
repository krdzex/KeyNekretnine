using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agents;

namespace KeyNekretnine.Application.Core.Adverts.Commands.PauseAdvert;
internal sealed class PauseAdvertHandler : ICommandHandler<PauseAdvertCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAgentRepository _agentRepository;

    public PauseAdvertHandler(
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

    public async Task<Result> Handle(PauseAdvertCommand request, CancellationToken cancellationToken)
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


        var pauseResult = advert.Pause(_dateTimeProvider.Now);

        if (pauseResult.IsFailure)
        {
            return pauseResult;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}