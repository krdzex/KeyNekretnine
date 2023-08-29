using Contracts;
using Entities.DomainErrors;
using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Service.Contracts;
using Shared.Error;
using System.Net;
using System.Transactions;

namespace KeyNekretnine.Application.Core.Agencies.Commands.UpdateAgency;
internal sealed class UpdateAgencyHandler : ICommandHandler<UpdateAgencyCommand, Unit>
{
    private readonly IRepositoryManager _repository;
    private readonly IServiceManager _service;

    public UpdateAgencyHandler(IRepositoryManager repository, IServiceManager service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Result<Unit>> Handle(UpdateAgencyCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
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

            request.UpdateAgency.ImageUrl = await _repository.Agency.GetAgencyImage(request.AgencyId, cancellationToken);

            if (request.UpdateAgency.Image?.Length > 0)
            {
                var tempProfileImageUrl = request.UpdateAgency.ImageUrl;

                request.UpdateAgency.ImageUrl = await _service.ImageService.UploadImageOnCloudinary(request.UpdateAgency.Image);

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

            await _repository.Agency.UpdateAgency(request.UpdateAgency, request.AgencyId, cancellationToken);

            await _repository.Agency.DeleteLanguagesForAgency(request.AgencyId, cancellationToken);

            if (request.UpdateAgency.LanguageId is not null)
            {
                foreach (var languageId in request.UpdateAgency.LanguageId)
                {
                    await _repository.Agency.AddLanguageToAgency(languageId, request.AgencyId, cancellationToken);

                }
            }

            transaction.Complete();
        }
        return Unit.Value;
    }
}