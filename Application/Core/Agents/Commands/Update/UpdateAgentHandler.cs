using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Service.Contracts;
using Shared.Error;
using System.Net;
using System.Transactions;

namespace KeyNekretnine.Application.Core.Agents.Commands.Update;
internal sealed class UpdateAgentHandler : ICommandHandler<UpdateAgentCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IServiceManager _service;

    public UpdateAgentHandler(IRepositoryManager repository, IServiceManager service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Result<Unit>> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var agentExist = await _repository.Agent.DoesAgentExist(request.AgentId, cancellationToken);

            if (!agentExist)
            {
                return Result.Failure<Unit>(DomainErrors.Agent.AgentNotFound);
            }

            var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

            if (userId is null)
            {
                return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
            }

            var agencyId = await _repository.Agent.AgencyIdOfAgent(request.AgentId, cancellationToken);

            var isUserAgencyOwner = await _repository.Agency.IsUserAgencyOwner(userId, agencyId, cancellationToken);

            if (!isUserAgencyOwner)
            {
                return Result.Failure<Unit>(DomainErrors.Agency.NotOwnerError);
            }

            request.Agent.ImageUrl = await _repository.Agent.GetAgentImage(request.AgentId, cancellationToken);

            if (request.Agent.Image?.Length > 0)
            {
                var tempProfileImageUrl = request.Agent.ImageUrl;

                request.Agent.ImageUrl = await _service.ImageService.UploadImageOnCloudinary(request.Agent.Image);

                if (tempProfileImageUrl is not null)
                {
                    var publicId = _service.ImageService.GetPublicIdFromUrl(tempProfileImageUrl);

                    var deleteResult = await _service.ImageService.DeleteImageFromCloudinary(publicId);

                    if (deleteResult.StatusCode != HttpStatusCode.OK)
                    {
                        return Result.Failure<Unit>(DomainErrors.ImageService.ImageCouldntBeDeleted);
                    }
                }
            }

            await _repository.Agent.UpdateAgent(request.Agent, request.AgentId, cancellationToken);

            await _repository.Agent.DeleteLanguagesForAgent(request.AgentId, cancellationToken);

            if (request.Agent.LanguageId is not null)
            {
                foreach (var languageId in request.Agent.LanguageId)
                {
                    await _repository.Agent.AddLanguageToAgent(languageId, request.AgentId, cancellationToken);

                }
            }

            transaction.Complete();
        }
        return Unit.Value;
    }
}
