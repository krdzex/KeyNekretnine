using KeyNekretnine.Application.Abstraction.Authentication;
using KeyNekretnine.Application.Abstraction.Clock;
using KeyNekretnine.Application.Abstraction.Messaging;
using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Agents;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ChangeAgentForAdvert;
internal sealed class ChangeAgentForAdvertHandler : ICommandHandler<ChangeAgentForAdvertCommand>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAgencyRepository _agencyRepository;
    private readonly IUserContext _userContext;
    private readonly IAgentRepository _agentRepository;

    public ChangeAgentForAdvertHandler(
        IAdvertRepository advertRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IAgencyRepository agencyRepository,
        IUserContext userContext,
        IAgentRepository agentRepository)
    {
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _userContext = userContext;
        _agencyRepository = agencyRepository;
        _agentRepository = agentRepository;
    }

    public async Task<Result> Handle(ChangeAgentForAdvertCommand request, CancellationToken cancellationToken)
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

        var agency = await _agencyRepository.GetByOwnerIdWithAgentsAsync(_userContext.UserId, cancellationToken);

        if (agency is null)
        {
            return Result.Failure(AgencyErrors.NotFound);
        }

        if (!agency.Agents.Any(x => x.Id == request.NewAgentId))
        {
            return Result.Failure(AgencyErrors.AgentNotInAgency);
        }

        advert.ChangeAgent(request.NewAgentId, _dateTimeProvider.Now);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}