using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Service.Contracts;
using Shared.Error;

namespace Application.Core.Agencies.Commands.CreateAgent;
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

        if (request.Agent.Image?.Length > 0)
        {
            request.Agent.ImageUrl = await _service.ImageService.UploadImageOnCloudinary(request.Agent.Image);
        }

        await _repository.Agent.CreateAgent(request.Agent, cancellationToken);

        return Unit.Value;
    }
}