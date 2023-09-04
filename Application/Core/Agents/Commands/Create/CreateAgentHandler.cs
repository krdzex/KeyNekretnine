namespace KeyNekretnine.Application.Core.Agents.Commands.Create;
internal sealed class CreateAgentHandler : ICommandHandler<CreateAgentCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IServiceManager _service;

    public CreateAgentHandler(IRepositoryManager repository, IServiceManager service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Result<Unit>> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var agencyExist = await _repository.Agency.DoesAgencyExist(request.Agent.AgencyId, cancellationToken);

            if (!agencyExist)
            {
                return Result.Failure<Unit>(DomainErrors.Agency.AgencyNotFound);
            }

            var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

            if (userId is null)
            {
                return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
            }

            var isUserAgencyOwner = await _repository.Agency.IsUserAgencyOwner(userId, request.Agent.AgencyId, cancellationToken);

            if (!isUserAgencyOwner)
            {
                return Result.Failure<Unit>(DomainErrors.Agency.NotOwnerError);
            }

            var phoneNumber = await _repository.PhoneNumber.MakeNumber(request.Agent.NumberMaker, cancellationToken);

            if (phoneNumber is null)
            {
                return Result.Failure<Unit>(DomainErrors.Agent.BadPhoneNumber);
            }

            request.Agent.PhoneNumber = phoneNumber;

            if (request.Agent.Image?.Length > 0)
            {
                request.Agent.ImageUrl = await _service.ImageService.UploadImageOnCloudinary(request.Agent.Image);
            }

            var agentId = await _repository.Agent.CreateAgentAndReturnId(request.Agent, cancellationToken);

            if (request.Agent.LanguageId is not null)
            {
                foreach (var languageId in request.Agent.LanguageId)
                {
                    await _repository.Agent.AddLanguageToAgent(languageId, agentId, cancellationToken);
                }
            }

            transaction.Complete();
        }
        return Unit.Value;
    }
}