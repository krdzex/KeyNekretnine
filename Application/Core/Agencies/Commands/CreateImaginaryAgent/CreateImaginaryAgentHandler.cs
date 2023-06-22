using Application.Abstraction.Messaging;
using Contracts;
using Entities.DomainErrors;
using MediatR;
using Service.Contracts;
using Shared.Error;

namespace Application.Core.Agencies.Commands.CreateImaginaryAgent;
internal sealed class CreateImaginaryAgentHandler : ICommandHandler<CreateImaginaryAgentCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IServiceManager _service;

    public CreateImaginaryAgentHandler(IRepositoryManager repository, IServiceManager service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Result<Unit>> Handle(CreateImaginaryAgentCommand request, CancellationToken cancellationToken)
    {

        var agencyExist = await _repository.Agency.DoesAgencyExist(request.AgencyId, cancellationToken);

        if (!agencyExist)
        {
            return Result.Failure<Unit>(DomainErrors.Agency.AgencyNotFound);
        }

        var userId = await _repository.User.GetUserIdFromEmail(request.Email, cancellationToken);

        if (userId is null)
        {
            return Result.Failure<Unit>(DomainErrors.User.UserNotFound);
        }

        var isUserAgencyOwner = await _repository.Agency.IsUserAgencyOwner(userId, request.AgencyId, cancellationToken);

        if (!isUserAgencyOwner)
        {
            return Result.Failure<Unit>(DomainErrors.Agency.NotOwnerError);
        }

        if (request.ImaginaryAgent.Image?.Length > 0)
        {
            request.ImaginaryAgent.ImageUrl = await _service.ImageService.UploadImageOnCloudinary(request.ImaginaryAgent.Image);
        }

        await _repository.Agency.CreateImaginaryAgent(request.ImaginaryAgent, request.AgencyId, cancellationToken);

        return Unit.Value;
    }
}